//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// clsMTodoTareas.cs (24/03/2017)												\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Clase de las tareas de MTodo								\\
// Fecha Mod:		24/03/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System;
#endregion

namespace MoonAntonio.MTodo
{
	/// <summary>
	/// <para>Clase de las tareas de MTodo</para>
	/// </summary>
	[System.Serializable]
	public class clsMTodoTareas
	{
		#region Variables
		/// <summary>
		/// <para>Titulo de la tarea.</para>
		/// </summary>
		public string Titulo = string.Empty;								// Titulo de la tarea
		/// <summary>
		/// <para>Esta la tarea completada.</para>
		/// </summary>
		public bool Completado = false;										// Esta la tarea completada
		/// <summary>
		/// <para>Descripcion de la tarea.</para>
		/// </summary>
		public string Descripcion = string.Empty;							// Descripcion de la tarea.
		/// <summary>
		/// <para>Fecha actual.</para>
		/// </summary>
		public string Fecha = string.Empty;									// Fecha actual
		/// <summary>
		/// <para>Categoria de la tarea.</para>
		/// </summary>
		public string Categoria = string.Empty;								// Categoria de la tarea
		#endregion

		#region Constructor
		/// <summary>
		/// <para>MTodo Tareas.</para>
		/// </summary>
		/// <param name="titulo">Titulo de la tarea.</param>
		/// <param name="completado">La tarea esta completada.</param>
		/// <param name="descripcion">Descripcion de la tarea</param>
		/// <param name="categoria">Categoria de la tarea.</param>
		public clsMTodoTareas(string titulo, bool completado, string descripcion, string categoria)// MTodo Tareas
		{
			Titulo = titulo;
			Completado = completado;
			Descripcion = descripcion;
			Fecha = GetFecha();
			Categoria = categoria;
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Obtiene la fecha actual.</para>
		/// </summary>
		/// <returns>Fecha actual.</returns>
		public string GetFecha()// Obtiene la fecha actual
		{
			return DateTime.Now.ToString();
		}
		#endregion
	}
}
