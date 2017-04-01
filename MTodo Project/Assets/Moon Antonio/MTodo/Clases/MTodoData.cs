//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoData.cs (12/01/2017)													\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:	Data de MTodo								    				\\
// Fecha Mod:		24/03/2017													\\
// Ultima Mod:	    Agregado ToolEstado											\\
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
    /// <para>Data de MTodo	</para>
    /// </summary>
    [Serializable]
    public class MTodoData : ScriptableObject
    {
		#region Variables
		/// <summary>
		/// <para>Estado de la herramienta seleccionada.</para>
		/// </summary>
		public ToolEstado estado = ToolEstado.MTodo;										// Estado de la herramienta seleccionada
        /// <summary>
        /// <para>Tickets de MTodo</para>
        /// </summary>
        public List<clsMTodoTickets> Tickets = new List<clsMTodoTickets>();                 // Tickets de MTodo
        /// <summary>
        /// <para>Categorias de MTodo</para>
        /// </summary>
        public List<string> Categorias = new List<string>(){ "TODO", "BUG" };               // Categorias de MTodo
        /// <summary>
        /// <para>Auto escanear activado</para>
        /// </summary>
        public bool AutoEscaneoMTodo;                                                       // Auto escanear activado
        /// <summary>
        /// <para>Ruta de data</para>
        /// </summary>
        public string RutaDataMTodo = "";                                                   // Ruta de data
		/// <summary>
		/// <para>Version actual de MTodo que se usa actualmente.</para>
		/// </summary>
		public string versionActual = "2.0.1";												// Version actual de Mtodo que se usa actualmente
		/// <summary>
		/// <para>Version online de MTodo.</para>
		/// </summary>
		public string versionTop = "";														// Version online de MTodo
		#endregion

		#region API
		/// <summary>
		/// <para>Obtiene el conteo de los tickets</para>
		/// </summary>
		public int TicketsCount// Obtiene el conteo de los tickets
        {
            get { return Tickets.Count; }
        }

        /// <summary>
        /// <para>Obtiene el conteo de las categorias</para>
        /// </summary>
        public int CategoriasCount// Obtiene el conteo de las categorias
        {
            get { return Categorias.Count; }
        }

        /// <summary>
        /// <para>Obtiene el conteo de las categorias</para>
        /// </summary>
        /// <param name="cat">Categoria</param>
        /// <returns></returns>
        public int GetCountDeCategorias(int cat)// Obtiene el conteo de las categorias
        {
            return cat != -1 ? Tickets.Count(e => e.Categoria == Categorias[cat]) : TicketsCount;
        }

        /// <summary>
        /// <para>Obtiene el ticket deseado.</para>
        /// </summary>
        /// <param name="index">ID del ticket</param>
        /// <returns>Ticket</returns>
        public clsMTodoTickets GetTicketAt(int index)// Obtiene el ticket deseado
        {
            return Tickets[index];
        }

        /// <summary>
        /// <para>Agrega una categoria nueva.</para>
        /// </summary>
        /// <param name="cat">Nombre de la categoria.</param>
        public void AddCategoria(string cat)// Agrega una categoria nueva
        {
            // Si categorias contiene la categoria dad o si esta vacio volvemos
            if (Categorias.Contains(cat) || string.IsNullOrEmpty(cat))
            {
                return;
            }

            // Sino agregamos la nueva categoria
            Categorias.Add(cat);
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
        /// <para>Guarda los datos de MTodo</para>
        /// </summary>
        /// <param name="esca">Valor de AutoEscaneo</param>
        /// <param name="rut">Valor de Ruta</param>
        public void Guardado(bool esca,string rut)// Guarda los datos de MTodo
        {
            AutoEscaneoMTodo = esca;
            RutaDataMTodo = rut;
        }
        #endregion

    }

	public enum ToolEstado
	{
		MTodo,
		MTarea
	}
}