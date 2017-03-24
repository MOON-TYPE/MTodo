//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MTodoWindows.cs (18/02/2017)													\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Windows de preferencias de MTodo							\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio en el namespace										\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using MoonAntonio.MTodo.Extensiones;
#endregion

namespace MoonAntonio.MTodo
{
    /// <summary>
    /// <para>Windows de preferencias de MTodo</para>
    /// </summary>
	public class MTodoWindows : MonoBehaviour 
	{
        #region Variables
        /// <summary>
        /// <para>Ajustes cargados actualmente</para>
        /// </summary>
        private static bool ajustesCargados;
		/// <summary>
		/// <para>Auto escanear activado</para>
		/// </summary>
		public static bool autoEscaneo;
        /// <summary>
        /// <para>Ruta de data</para>
        /// </summary>
        public static string dataPath = @"Assets/Moon Antonio/MTodo/Data/MTodoData.asset";
		/// <summary>
		/// <para>Ruta de data</para>
		/// </summary>
		public static string dataPathTarea = @"Assets/Moon Antonio/MTodo/Data/MTareaData.asset";
		/// <summary>
		/// <para>Data de MTodo</para>
		/// </summary>
		private static MTodoData data;
		/// <summary>
		/// <para>Data de MTodo</para>
		/// </summary>
		private static MTodoTareaData dataTarea;
		#endregion

		#region Gui
		[PreferenceItem("MToDo")]
        public static void ToDoWindowsGUI()
        {
            if (!ajustesCargados)
                CargarAjustes();

            ajustesCargados = true;

            autoEscaneo = EditorGUILayout.Toggle("Auto Escaneo", autoEscaneo);
            if (autoEscaneo == true)
            {
                EditorGUILayout.HelpBox("Esto puede ocasionar lentitud en proyectos grandes.", MessageType.Warning);
            }

            using (new MTodoExtensiones.HorizontalBlock())
            {
				using (new MTodoExtensiones.VerticalBlock())
				{
					GUILayout.Label("MTodoData  :");
					if (GUILayout.Button("Buscar", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
						dataPath = MTodoExtensiones.GlobalPathARelativa(EditorUtility.SaveFilePanel("", "Assets", "MTodoData", "asset"));
				}

				using (new MTodoExtensiones.VerticalBlock())
				{
					GUILayout.Label(dataPath, GUILayout.ExpandWidth(true));
				}

            }

			using (new MTodoExtensiones.HorizontalBlock())
			{
				using (new MTodoExtensiones.VerticalBlock())
				{
					GUILayout.Label("MTareaData :");
					if (GUILayout.Button("Buscar", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
						dataPath = MTodoExtensiones.GlobalPathARelativa(EditorUtility.SaveFilePanel("", "Assets", "MTareaData", "asset"));
				}

				using (new MTodoExtensiones.VerticalBlock())
				{
					GUILayout.Label(dataPathTarea, GUILayout.ExpandWidth(true));
				}
			}

			EditorGUILayout.HelpBox("Version MTodo<" + data.versionActual + ">", MessageType.Info);

			if (GUI.changed)
                GuardarAjustes();

        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Carga los ajustes de MTodo</para>
        /// </summary>
        private static void CargarAjustes()// Carga los ajustes de MTodo
		{
            data = (MTodoData)MTodoExtensiones.CrearDataPersistente<MTodoData>(dataPath);
			dataTarea = MTodoExtensiones.CrearDataPersistente<MTodoTareaData>(dataPathTarea);
            autoEscaneo = data.AutoEscaneoMTodo;
            dataPath = data.RutaDataMTodo;
			dataPathTarea = dataTarea.RutaDataMTodoTareas;
        }

        /// <summary>
        /// <para>Aplica los ajustes de MTodo</para>
        /// </summary>
        private static void GuardarAjustes()// Aplica los ajustes de MTodo
		{
            data = MTodoExtensiones.CrearDataPersistente<MTodoData>(dataPath);
            data.AutoEscaneoMTodo = autoEscaneo;
            data.RutaDataMTodo = dataPath;
			dataTarea = MTodoExtensiones.CrearDataPersistente<MTodoTareaData>(dataPathTarea);
		}
        #endregion
    }
}
