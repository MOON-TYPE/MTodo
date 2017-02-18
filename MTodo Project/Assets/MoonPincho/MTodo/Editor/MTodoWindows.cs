//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MTodoWindows.cs (18/02/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Windows de preferencias de MTodo							\\
// Fecha Mod:		18/02/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using MoonPincho.MTodo.Extensiones;
#endregion

namespace MoonPincho.MTodo
{
    /// <summary>
    /// <para>Windows de preferencias de MTodo</para>
    /// </summary>
	public class MTodoWindows : MonoBehaviour 
	{
        #region Variables privadas
        /// <summary>
        /// <para>Ajustes cargados actualmente</para>
        /// </summary>
        private static bool ajustesCargados;
        /// <summary>
        /// <para>Auto escanear activado</para>
        /// </summary>
        private static bool autoEscaneo;
        /// <summary>
        /// <para>Ruta de data</para>
        /// </summary>
        private static string dataPath = @"Assets/MoonPincho/MTodo/Data/MTodoData.asset";
        /// <summary>
        /// <para>Data de MTodo</para>
        /// </summary>
        private static MTodoData data;
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
                GUILayout.Label(dataPath, GUILayout.ExpandWidth(true));
                if (GUILayout.Button("Buscar", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
                    dataPath = MTodoExtensiones.GlobalPathARelativa(EditorUtility.SaveFilePanel("", "Assets", "MTodoData", "asset"));
            }

            if (GUI.changed)
                GuardarAjustes();

        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Carga los ajustes de MTodo</para>
        /// </summary>
        private static void CargarAjustes()
        {
            data = (MTodoData)MTodoExtensiones.CrearDataPersistente<MTodoData>(dataPath);
            autoEscaneo = data.AutoEscaneoMTodo;
            dataPath = data.RutaDataMTodo;
        }

        /// <summary>
        /// <para>Aplica los ajustes de MTodo</para>
        /// </summary>
        private static void GuardarAjustes()
        {
            data = MTodoExtensiones.CrearDataPersistente<MTodoData>(dataPath);
            data.AutoEscaneoMTodo = autoEscaneo;
            data.RutaDataMTodo = dataPath;
        }
        #endregion
    }
}
