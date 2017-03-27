//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// clsMTodoTareaCategoria.cs (27/03/2017)										\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Clase de las categorias de las tareas						\\
// Fecha Mod:		27/03/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using System.Collections.Generic;
#endregion

namespace MoonAntonio.MTodo
{
	/// <summary>
	/// <para>Clase de las categorias de las tareas</para>
	/// </summary>
	[System.Serializable]
	public class clsMTodoTareaCategoria
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Lista de las categorias.</para>
		/// </summary>
		public List<clsMTodoTareas> categorias;							// Lista de las categorias
		/// <summary>
		/// <para>Nombre de la categoria.</para>
		/// </summary>
		public string nombre;                                           // Nombre de la categoria
		#endregion

		#region Constructor
		/// <summary>
		/// <para>Inicializa una nueva instancia de la clase <see cref="clsMTodoTareaCategoria"/></para>
		/// </summary>
		/// <param name="nomCat">Nombre de la categoria</param>
		public clsMTodoTareaCategoria(string nomCat)// Inicializa una nueva instancia de la clase <see cref="clsMTodoTareaCategoria"/>
		{
			this.nombre = nomCat;
			this.categorias = new List<clsMTodoTareas>();
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Agrega una tarea.</para>
		/// </summary>
		/// <param name="nuevaTarea">La nueva tarea.</param>
		public void AddTarea(clsMTodoTareas nuevaTarea)// Agrega una tarea
		{
			this.categorias.Add(nuevaTarea);
		}

		/// <summary>
		/// <para>Quita una tarea.</para>
		/// </summary>
		/// <param name="tarea">Tarea que quitar.</param>
		public void RemoveTarea(clsMTodoTareas tarea)// Quita una tarea
		{
			this.categorias.Remove(tarea);
		}
		#endregion


	}
}
