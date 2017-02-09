//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MTodoExtensiones.cs (08/02/2017)												\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Extensiones para el editor de MTodo								\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
#endregion

namespace MoonPincho.MTodo.Extensiones
{
	public static class MTodoExtensiones
	{
        #region Scriptable
        /// <summary>
        /// <para>Crea la Data de MTodo</para>
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="ruta">Ruta de la Data</param>
        /// <returns>La data creada</returns>
        public static T CrearData<T>(string ruta) where T : ScriptableObject
        {
            var temp = ScriptableObject.CreateInstance<T>();

            AssetDatabase.CreateAsset(temp, ruta);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            return temp;
        }

        /// <summary>
        /// <para>Carga la data de MTodo</para>
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="ruta">Ruta de MTodo data</param>
        /// <returns>La Data de MTodo</returns>
        public static T CargarData<T>(string ruta) where T : ScriptableObject
        {
            return AssetDatabase.LoadAssetAtPath<T>(ruta);
        }

        /// <summary>
        /// <para>Borra la Data de MTodo</para>
        /// </summary>
        /// <param name="ruta">Ruta de la data</param>
        /// <returns>True si se ha borrado, false si no se ha borrado</returns>
        public static bool BorrarData(string ruta)
        {
            var archivo = new FileInfo(ruta);
            if (archivo.Exists)
            {
                archivo.Delete();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <para>Carga o en su defecto crea una Data de MTodo</para>
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="ruta">Ruta de MTodo</param>
        /// <returns>La carga</returns>
        public static T CrearDataPersistente<T>(string ruta) where T : ScriptableObject
        {
            var temp = CargarData<T>(ruta);
            return temp ?? CrearData<T>(ruta);
        }
        #endregion

#if UNITY_EDITOR
        #region Layout
        public class VerticalBlock : IDisposable
        {
            public VerticalBlock(params GUILayoutOption[] opciones)
            {
                GUILayout.BeginVertical(opciones);
            }

            public VerticalBlock(GUIStyle style, params GUILayoutOption[] opciones)
            {
                GUILayout.BeginVertical(style, opciones);
            }

            public void Dispose()
            {
                GUILayout.EndVertical();
            }
        }

        public class ScrollviewBlock : IDisposable
        {
            public ScrollviewBlock(ref Vector2 scrollPos, params GUILayoutOption[] opciones)
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos, opciones);
            }

            public void Dispose()
            {
                GUILayout.EndScrollView();
            }
        }

        public class HorizontalBlock : IDisposable
        {
            public HorizontalBlock(params GUILayoutOption[] opciones)
            {
                GUILayout.BeginHorizontal(opciones);
            }

            public HorizontalBlock(GUIStyle style, params GUILayoutOption[] opciones)
            {
                GUILayout.BeginHorizontal(style, opciones);
            }

            public void Dispose()
            {
                GUILayout.EndHorizontal();
            }
        }

        public class ColoredBlock : System.IDisposable
        {
            public ColoredBlock(Color color)
            {
                GUI.color = color;
            }

            public void Dispose()
            {
                GUI.color = Color.white;
            }
        }
        #endregion
#endif
    }
}