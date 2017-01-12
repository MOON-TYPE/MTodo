//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoEditor.cs (12/01/1991)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Editor de MTodo													\
// Fecha Mod:		12/01/1991													\\
// Ultima Mod:	Version Inicial													\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace MoonPincho.MTodo
{
    /// <summary>
    /// <para>Editor de MTodo</para>
    /// </summary>
    public class MTodoEditor : EditorWindow
    {
        #region Variables
        public static string pathIconBase = "Asset/MoonPincho/Editor/Icon/MTodoIcon.png";
        #endregion

        #region Inicializadores
        /// <summary>
        /// <para>Inicializar el editor</para>
        /// </summary>
        [MenuItem("Moon Pincho/MTodo")]
        public static void Init()//Inicializar el editor
        {
            Texture icono = AssetDatabase.LoadAssetAtPath<Texture>(pathIconBase);
            var window = GetWindow<MTodoEditor>();
            window.minSize = new Vector2(0, 0);
            GUIContent tituloContenido = new GUIContent("MTodo",icono);
            window.titleContent = tituloContenido;
            window.Show();
        }

        /// <summary>
        /// <para></para>
        /// </summary>
        private void OnEnable()
        {
            // Comprobar si el editor esta reproduciendo

            // Refrescar archivos

            // Cargar data

            // Activar observador
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
    }
}