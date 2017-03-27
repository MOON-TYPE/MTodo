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
using System.Linq;
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
        public List<clsMTodoTareaCategoria> Categorias = new List<clsMTodoTareaCategoria>();// Categorias de las tareas de MTodo
        /// <summary>
        /// <para>Ruta de data</para>
        /// </summary>
        public string RutaDataMTodoTareas = "";                                             // Ruta de data
		#endregion

		#region API
		/// <summary>
		/// <para>Obtiene el conteo de las tareas.</para>
		/// </summary>
		public int TareasCount// Obtiene el conteo de las tareas
		{
			get { return Tareas.Count; }
		}

		/// <summary>
		/// <para>Obtiene el conteo de las categorias</para>
		/// </summary>
		public int CategoriasCount// Obtiene el conteo de las categorias
		{
			get { return Categorias.Count; }
		}

		/// <summary>
		/// <para>Obtiene la tarea deseada.</para>
		/// </summary>
		/// <param name="index">ID de la tarea</param>
		/// <returns>Ticket</returns>
		public clsMTodoTareas GetTareaAt(int index)// Obtiene la tarea deseada
		{
			return Tareas[index];
		}

		/// <summary>
		/// <para>Agrega una categoria nueva.</para>
		/// </summary>
		/// <param name="cat">Nombre de la categoria.</param>
		public void AddCategoria(string cat)// Agrega una categoria nueva
		{
			// TODO Funcionalidad Add Categoria
		}

		/// <summary>
		/// <para>Quita una categoria</para>
		/// </summary>
		/// <param name="index">ID de la categoria.</param>
		public void RemoveCategoria(int index)// Quita una categoria
		{
			// Si las categorias son mayores que el index pasado
			if (Categorias.Count >= (index + 1))
			{
				// Quitamos la categoria dada
				Categorias.RemoveAt(index);
			}
		}

		/// <summary>
		/// <para>Guarda los datos de las tareas</para>
		/// </summary>
		/// <param name="rut">Valor de Ruta</param>
		public void Guardado(string rut)// Guarda los datos de las tareas
		{
			RutaDataMTodoTareas = rut;
		}
		#endregion
	}
}
