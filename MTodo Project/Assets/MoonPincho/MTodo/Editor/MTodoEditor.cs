//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoEditor.cs (12/01/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Editor de MTodo													\
// Fecha Mod:		08/02/2017													\\
// Ultima Mod:	Interfaz grafica												\\
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
    public class MTodoEditor : EditorWindow
    {
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
        }
        #endregion

        #region GUI
        /// <summary>
        /// <para></para>
        /// </summary>
        private void OnGUI()
        {
            // Comprobar la data
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
}