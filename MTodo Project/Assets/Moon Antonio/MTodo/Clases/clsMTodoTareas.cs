//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// clsMTodoTareas.cs (24/03/2017)												\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Clase de las tareas de MTodo								\\
// Fecha Mod:		24/03/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using System;
using System.Collections.Generic;
#endregion

namespace MoonAntonio.MTodo
{
	/// <summary>
	/// <para>Clase de las tareas de MTodo</para>
	/// </summary>
	[System.Serializable]
	public class clsMTodoTareas
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Fecha de inicio de la tarea.</para>
		/// </summary>
		public DateTime fechaInicio;								// Fecha de inicio de la tarea
		/// <summary>
		/// <para>Fecha de fin de la tarea.</para>
		/// </summary>
		public DateTime fechaFin;									// Fecha de fin de la tarea
		/// <summary>
		/// <para>Nombre de la tarea.</para>
		/// </summary>
		public string nombre;										// Nombre de la tarea
		/// <summary>
		/// <para>Descripcion de la tarea.</para>
		/// </summary>
		public string descripcion;									// Descripcion de la tarea
		/// <summary>
		/// <para>Estado de la tarea.</para>
		/// </summary>
		public Estado estadoActual;                                 // Estado de la tarea
		#endregion

		#region Constructores
		/// <summary>
		/// <para>Inicializa una nueva instancia de la clase <see cref="clsMTodoTareas"/>.</para>
		/// </summary>
		/// <param name="nuevoNombre">El nombre de la tarea.</param>
		/// <param name="nuevaDesc">La descripcion de la tarea.</param>
		public clsMTodoTareas(string nuevoNombre, string nuevaDesc)// Inicializa una nueva instancia de la clase <see cref="clsMTodoTareas"/>
		{
			this.nombre = nuevoNombre;
			this.descripcion = nuevaDesc;
			this.fechaInicio = DateTime.Now;
			this.estadoActual = Estado.Abierta;
		}

		/// <summary>
		/// <para>Inicializa una nueva instancia de la clase <see cref="clsMTodoTareas"/>.</para>
		/// </summary>
		/// <param name="nuevoNombre">El nombre de la tarea.</param>
		public clsMTodoTareas(string nuevoNombre)// Inicializa una nueva instancia de la clase <see cref="clsMTodoTareas"/>
		{
			this.nombre = nuevoNombre;
			this.descripcion = "Escribe la tarea";
			this.fechaInicio = DateTime.Now;
			this.estadoActual = Estado.Abierta;
		}

		/// <summary>
		/// <para>Inicializa una nueva instancia de la clase <see cref="clsMTodoTareas"/>.</para>
		/// </summary>
		public clsMTodoTareas()// Inicializa una nueva instancia de la clase <see cref="clsMTodoTareas"/>
		{
			this.nombre = "Nueva Tarea";
			this.descripcion = "Escribe la tarea";
			this.fechaInicio = DateTime.Now;
			this.estadoActual = Estado.Abierta;
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Cerrar la tarea.</para>
		/// </summary>
		public void Completado()// Cerrar la tarea
		{
			if (this.estadoActual == Estado.Abierta)
			{
				this.estadoActual = Estado.Cerrada;
				this.fechaFin = DateTime.Now;
			}
		}
		#endregion

		#region Enum
		/// <summary>
		/// <para>Estado de la tarea.</para>
		/// </summary>
		public enum Estado
		{
			Abierta,
			Cerrada
		}
		#endregion
	}
}
