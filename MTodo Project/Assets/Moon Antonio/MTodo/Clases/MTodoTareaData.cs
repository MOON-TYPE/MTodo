//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MTodoTareaData.cs (24/03/2017)												\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Data de tareas de MTodo										\\
// Fecha Mod:		24/03/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System;
using System.Collections.Generic;
#endregion

namespace MoonAntonio.MTodo
{
	/// <summary>
	/// <para>Data de tareas de MTodo</para>
	/// </summary>
	[Serializable]
	public class MTodoTareaData : ScriptableObject
	{
		#region Variables
        /// <summary>
        /// <para>Tareas de MTodo</para>
        /// </summary>
        public List<clsMTodoTareas> Tareas = new List<clsMTodoTareas>();					// Tareas de MTodo
        /// <summary>
        /// <para>Categorias de las tareas de MTodo</para>
        /// </summary>
        public List<string> Categorias = new List<string>(){ "Default", "Urgente" };            // Categorias de las tareas de MTodo
        /// <summary>
        /// <para>Ruta de data</para>
        /// </summary>
        public string RutaDataMTodoTareas = "";                                             // Ruta de data
		#endregion
	}
}
