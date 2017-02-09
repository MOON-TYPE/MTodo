//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// clsMtodoParser.cs (09/02/2017)												\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Clase para analizar 										\\
// Fecha Mod:		09/02/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
#endregion

namespace MoonPincho.MTodo
{
	public class clsMtodoParser
	{
        #region Variables Privadas
        /// <summary>
        /// <para>Ruta del archivo</para>
        /// </summary>
        private string rutaArch;                                        // Ruta del archivo
        /// <summary>
        /// <para>Texto del archivo</para>
        /// </summary>
        private string texto;                                           // Texto del archivo
        /// <summary>
        /// <para>Categorias del archivo</para>
        /// </summary>
        private string[] categorias;                                    // Categorias del archivo
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Constructor de clsMtodoParser</para>
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo</param>
        /// <param name="cat">Categoria del archivo</param>
        public clsMtodoParser(string rutaArchivo, string[] cat = null)// Constructor de clsMtodoParser
        {
            rutaArch = rutaArchivo;
            var archivo = new FileInfo(rutaArch);
            if (archivo.Exists)
            {
                texto = File.ReadAllText(rutaArchivo);
            }
            categorias = cat;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Analisis</para>
        /// </summary>
        /// <returns></returns>
        public clsMTodoTickets[] Parse()// Analisis
        {
            var archivo = new FileInfo(rutaArch);

            if (archivo.Exists == false)
                return null;

            var temp = new List<clsMTodoTickets>();

            foreach (var cat in categorias)
            {
                var matches = Regex.Matches(texto, string.Format(@"(?<=\W|^)//(\s?{0})(.*)", cat));
                temp.AddRange(from Match match in matches let text = match.Groups[2].Value let line = IndexALinea(match.Index) select new clsMTodoTickets(text, "", cat, rutaArch, line));
            }
            return temp.ToArray();
        }

        /// <summary>
        /// <para>Indexa una linea</para>
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns></returns>
        private int IndexALinea(int index)// Indexa una linea
        {
            return texto.Take(index).Count(c => c == '\n') + 1;
        }
        #endregion
    }
}