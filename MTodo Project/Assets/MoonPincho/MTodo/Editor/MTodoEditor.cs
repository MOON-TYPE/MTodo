//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoEditor.cs (12/01/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Editor de MTodo													\
// Fecha Mod:		10/03/2017													\\
// Ultima Mod:	Implementado control de versiones								\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonPincho.MTodo.Extensiones;
#endregion

namespace MoonPincho.MTodo
{
    /// <summary>
    /// <para>Editor de MTodo</para>
    /// </summary>
	[ExecuteInEditMode]
    public class MTodoEditor : EditorWindow
    {
		// TODO Test Editor

        #region Variables Privadas
        /// <summary>
        /// <para>Archivos de MTodo</para>
        /// </summary>
        private FileInfo[] archivos;                                                    // Archivos de MTodo
        /// <summary>
        /// <para>Data de MTodo</para>
        /// </summary>
        private MTodoData data;                                                         // Data de MTodo
        /// <summary>
        /// <para>Ruta de MTodo</para>
        /// </summary>
        private string rutaData = @"Assets/MoonPincho/MTodo/Data/MTodoData.asset";      // Ruta de MTodo
        /// <summary>
        /// <para>Tickets que seran mostrados</para>
        /// </summary>
        private clsMTodoTickets[] ticketsMostrados;                                     // Tickets que seran mostrados
        /// <summary>
        /// <para>Categoria actual a mostrar</para>
        /// </summary>
        private int catActual = -1;                                                     // Categoria actual a mostrar
        /// <summary>
        /// <para>Palabra que buscar</para>
        /// </summary>
        private string buscaString = "";                                                // Palabra que buscar
        public string BuscaString
        {
            get { return buscaString; }
            set { if (value != buscaString)
                {
                    buscaString = value;
                    RefrescarData();
                }

            }
        }
        /// <summary>
        /// <para>Observador de MTodo</para>
        /// </summary>
        private FileSystemWatcher observador;                                           // Observador de MTodo
        /// <summary>
        /// <para>Categorias</para>
        /// </summary>
        private string[] Categorias                                                     // Categorias
        {
            get
            {
                if (data != null && data.Categorias.Count > 0)
                    return data.Categorias.ToArray();
                else
                    return new string[] { "TODO", "BUG" };
            }
        }
        /// <summary>
        /// <para>Scroll del sidebar</para>
        /// </summary>
        private Vector2 sidebarScroll;                                                  // Scroll del sidebar
        /// <summary>
        /// <para>Scroll del area central</para>
        /// </summary>
        private Vector2 mainAreaScroll;                                                 // Scroll del area central
        /// <summary>
        /// <para>Nombre de la nueva categoria</para>
        /// </summary>
        private string nuevaCategoria = "";                                             // Nombre de la nueva categoria
        private float SidebarWidth
        {
            get { return position.width / 3f; }
        }
		/// <summary>
		/// <para>Temp actualizador.</para>
		/// </summary>
		private int updates = 0;														// Temp actualizador
		#endregion

		#region Inicializadores
		/// <summary>
		/// <para>Inicializar el editor</para>
		/// </summary>
		[MenuItem("Moon Pincho/MTodo")]
        public static void Init()//Inicializar el editor
        {
            Texture icono = AssetDatabase.LoadAssetAtPath<Texture>("Assets/MoonPincho/MTodo/Icon/MTodoIcon.png");
            var window = GetWindow<MTodoEditor>();
            window.minSize = new Vector2(0, 0);
            GUIContent tituloContenido = new GUIContent(" MTodo",icono);
            window.titleContent = tituloContenido;
            window.Show();
        }

        /// <summary>
        /// <para>Primera ejecucion de MTodoEditor</para>
        /// </summary>
        private void OnEnable()// Primera ejecucion de MTodoEditor
        {
            // Comprobar si el editor esta reproduciendo
            if (EditorApplication.isPlayingOrWillChangePlaymode == true)
                return;

            // Refrescar archivos
            RefrescarArchivos();

            // Cargar data
            data = MTodoExtensiones.CrearDataPersistente<MTodoData>(rutaData);

            // Refrescar Data
            RefrescarData();

            // Activar observador
            observador = new FileSystemWatcher(Application.dataPath, "*.cs");
            observador.Changed += Observador_Changed;
            observador.Deleted += Observador_Deleted;
            observador.Renamed += Observador_Renamed;
            observador.Created += Observador_Created;

            observador.EnableRaisingEvents = true;
            observador.IncludeSubdirectories = true;

			// Actualizador del editor
			EditorApplication.update = Actualizador;
		}
        #endregion

        #region GUI
        /// <summary>
        /// <para></para>
        /// </summary>
        private void OnGUI()
        {
            if(data == null)
            {
                GUILayout.Label("No se ha cargado la Data", EditorStyles.centeredGreyMiniLabel);
                return;
            }

            Undo.RecordObject(data, "tododata");

            Toolbar();
            using (new MTodoExtensiones.HorizontalBlock())
            {
                Sidebar();
                MainArea();
            }

            ProcesadorDelInput();

            EditorUtility.SetDirty(data);
        }

        private void Toolbar()
        {
            using (new MTodoExtensiones.HorizontalBlock(EditorStyles.toolbar))
            {
                GUILayout.Label("MToDo");
                if (GUILayout.Button("Escanear", EditorStyles.toolbarButton))
                    EscanearTodosLosArchivos();

                //EditorGUILayout.Slider(0.5f, 0, 1);
                GUILayout.FlexibleSpace();
                BuscaString = BuscarCampo(BuscaString, GUILayout.Width(250));
            }
        }

        private void Sidebar()
        {
            using (new MTodoExtensiones.VerticalBlock(GUI.skin.box, GUILayout.Width(SidebarWidth), GUILayout.ExpandHeight(true)))
            {
                using (new MTodoExtensiones.ScrollviewBlock(ref sidebarScroll))
                {
                    CategoriaCampo(-1);
                    for (var i = 0; i < data.CategoriasCount; i++)
                        CategoriaCampo(i);
                }
                AddCategoria();
            }
        }

        private void MainArea()
        {
            using (new MTodoExtensiones.VerticalBlock(GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
            {
                using (new MTodoExtensiones.ScrollviewBlock(ref mainAreaScroll)) 
                    for (var i = 0; i < ticketsMostrados.Length; i++)
                        TicketsCampo(i);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Refrescar los archivos en MTodo del proyecto</para>
        /// </summary>
        private void RefrescarArchivos()// Refrescar los archivos en MTodo del proyecto
        {
            // Variable del directorio
            var dirAssets = new DirectoryInfo(Application.dataPath);

            // Obten los archivos con la extension cs
            archivos = dirAssets.GetFiles("*.cs", SearchOption.AllDirectories).ToArray();
        }

        /// <summary>
        /// <para>Refresca la informacion de la Data</para>
        /// </summary>
        private void RefrescarData()// Refresca la informacion de la Data
        {
            // Comprobacion
            if (catActual == -1)
            {
                // Todas las cat
                ticketsMostrados = data.Tickets.ToArray();
            }
            else if (catActual >= 0)
            {
                // Solo las de las categorias correspondientes
                ticketsMostrados = data.Tickets.Where(e => e.Categoria == data.Categorias[catActual]).ToArray();
            }

            // Condicion vacia
            if (!string.IsNullOrEmpty(""))
            {
                var temp = ticketsMostrados;
                ticketsMostrados = temp.Where(e => e.Texto.Contains("")).ToArray();
            }
        }

        /// <summary>
        /// <para>Refresca los tickest que se muestran</para>
        /// </summary>
        private void RefrescaTicketsAMostrar()// Refresca los tickest que se muestran
        {
            if (catActual == -1)
                ticketsMostrados = data.Tickets.ToArray();
            else if (catActual >= 0)
                ticketsMostrados = data.Tickets.Where(e => e.Categoria == data.Categorias[catActual]).ToArray();
            if (!string.IsNullOrEmpty(BuscaString))
            {
                var temp = ticketsMostrados;
                ticketsMostrados = temp.Where(e => e.Texto.Contains(buscaString)).ToArray();
            }
        }

        /// <summary>
        /// <para>Escanea un archivo</para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo</param>
        private void EscanearArchivo(string rutaArchivo)// Escanea un archivo
        {
            var archivo = new FileInfo(rutaArchivo);

            // Existe el archivo
            if (archivo.Exists == false)
                return;

            var tickets = new List<clsMTodoTickets>();
            data.Tickets.RemoveAll(e => e.Archivo == rutaArchivo);

            // Parse Extension
            var parser = new clsMtodoParser(rutaArchivo, Categorias);

            // Except
            tickets.AddRange(parser.Parse());
            var temp = tickets.Except(data.Tickets);
            data.Tickets.AddRange(temp);
        }

        /// <summary>
        /// <para>Escanea todos los archivos</para>
        /// </summary>
        private void EscanearTodosLosArchivos()// Escanea todos los archivos
        {
            RefrescarArchivos();
            foreach (var archivo in archivos.Where(archivo => archivo.Exists))
            {
                EscanearArchivo(archivo.FullName);
            }
        }

        /// <summary>
        /// <para>Agrega una nueva categoria</para>
        /// </summary>
        private void AddCategoria()// Agrega una nueva categoria
        {
            using (new MTodoExtensiones.HorizontalBlock(EditorStyles.helpBox))
            {
                nuevaCategoria = EditorGUILayout.TextField(nuevaCategoria);
                if (GUILayout.Button("+", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
                {
                    data.AddCategoria(nuevaCategoria);
                    nuevaCategoria = "";
                    GUI.FocusControl(null);
                }
            }
        }

        /// <summary>
        /// <para>Procesa el input del mouse</para>
        /// </summary>
        private void ProcesadorDelInput()// Procesa el input del mouse
        {
            if (Event.current.type == EventType.MouseDown)
            {
                if (Event.current.button == 0) {}

                if (Event.current.button == 1)
                {
                    ClickMouseDerecho();
                }

            }

        }
		#endregion

		#region Funcionalidad
		/// <summary>
		/// <para>Actualizador del Editor.</para>
		/// </summary>
		private void Actualizador()// Actualizador del Editor
		{
			updates++;
			if (updates > 200)
			{
				updates = 0;
			}
		}

		/// <summary>
		/// <para>Busca en el campo de las categorias</para>
		/// </summary>
		/// <param name="index">ID de la categoria</param>
		private void CategoriaCampo(int index)// Busca en el campo de las categorias
        {
            Event e = Event.current;
            var cat = index == -1 ? "Todas las Categorias" : data.Categorias[index];
            using (new MTodoExtensiones.HorizontalBlock(EditorStyles.helpBox))
            {
                using (new MTodoExtensiones.ColoredBlock(index == catActual ? Color.green : Color.white))
                {
                    GUILayout.Label("#" + cat);
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("(" + data.GetCountDeCategorias(index) + ")");
                }
                if (index != -1 && index != 0 && index != 1)
                {
                    if (GUILayout.Button("x", EditorStyles.miniButton))
                        EditorApplication.delayCall += () =>
                        {
                            data.RemoveCategoria(index);
                            Repaint();
                        };
                }
            }
            var rect = GUILayoutUtility.GetLastRect();
            if (e.isMouse && e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
                SetCatActual(index);
        }

        /// <summary>
        /// <para>Busca en un campo</para>
        /// </summary>
        /// <param name="buscarPalabr">Palabra que buscar</param>
        /// <param name="opciones">Opciones de layout</param>
        /// <returns>La palabra encontrada</returns>
        private string BuscarCampo(string buscarPalabr, params GUILayoutOption[] opciones)// Busca en un campo
        {
            buscarPalabr = GUILayout.TextField(buscarPalabr, "ToolbarSeachTextField", opciones);
            if (GUILayout.Button("", "ToolbarSeachCancelButton"))
            {
                buscarPalabr = "";
                GUI.FocusControl(null);
            }
            return buscarPalabr;
        }

        /// <summary>
        /// <para>Busca el ticket en un campo</para>
        /// </summary>
        /// <param name="index"></param>
        private void TicketsCampo(int index)// Busca el ticket en un campo
        {
            var entry = ticketsMostrados[index];
            using (new MTodoExtensiones.VerticalBlock(EditorStyles.helpBox))
            {
                using (new MTodoExtensiones.HorizontalBlock())
                {
                    GUILayout.Label(entry.Categoria, EditorStyles.boldLabel);
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(entry.PathAMostrar, EditorStyles.miniBoldLabel);
                }
                GUILayout.Space(5f);
                GUILayout.Label(entry.Texto, EditorStyles.largeLabel);
            }
            Event e = Event.current;
            var rect = GUILayoutUtility.GetLastRect();
            if (e.isMouse && e.type == EventType.MouseDown && rect.Contains(e.mousePosition) && e.clickCount == 2)
                EditorApplication.delayCall += () =>
                {
                    UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(entry.Archivo, entry.Linea);
                };
        }

        /// <summary>
        /// <para>Fija la categoria actual</para>
        /// </summary>
        /// <param name="index">ID Categoria</param>
        private void SetCatActual(int index)// Fija la categoria actual
        {
            EditorApplication.delayCall += () =>
            {
                catActual = index;
                RefrescaTicketsAMostrar();
                Repaint();
            };
        }

        /// <summary>
        /// <para>Click Derecho con el mouse</para>
        /// </summary>
        private void ClickMouseDerecho()// Click Derecho con el mouse
        {
            GenericMenu menu = new GenericMenu();

            //menu.AddSeparator("");
            menu.AddItem(new GUIContent("GitHub"), false, SubMenuCallBack, TODOMenuItem.GitHub);

            menu.ShowAsContext();
        }

        /// <summary>
        /// <para>Llamadas desde el submenu</para>
        /// </summary>
        /// <param name="obj"></param>
        private void SubMenuCallBack(object obj)// Llamadas desde el submenu
        {
            TODOMenuItem item = (TODOMenuItem)obj;
            switch (item)
            {
                case TODOMenuItem.GitHub:
                    Application.OpenURL("https://github.com/lPinchol/MTodo");
                    break;
            }
        }


        #endregion

        #region Eventos Handles
        private void Observador_Created(object obj, FileSystemEventArgs e)
        {
            EditorApplication.delayCall += () => EscanearArchivo(e.FullPath);
        }

        private void Observador_Renamed(object obj, RenamedEventArgs e)
        {
            EditorApplication.delayCall += () => EscanearArchivo(e.FullPath);
        }

        private void Observador_Deleted(object obj, FileSystemEventArgs e)
        {
            EditorApplication.delayCall += () => data.Tickets.RemoveAll(en => en.Archivo == e.FullPath);
        }

        private void Observador_Changed(object obj, FileSystemEventArgs e)
        {
            EditorApplication.delayCall += () => EscanearArchivo(e.FullPath);
        }
        #endregion

    }

    /// <summary>
    /// <para>Enum del contenido del menu</para>
    /// </summary>
    public enum TODOMenuItem
    {
        GitHub,
    }
}