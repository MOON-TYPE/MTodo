//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// MTodoData.cs (12/01/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:	Clase de los tickets de MTodo								    \\
// Fecha Mod:		12/01/2017													\\
// Ultima Mod:	Version Inicial													\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonPincho.MTodo
{
    /// <summary>
    /// <para>Clase de los tickets de MTodo</para>
    /// </summary>
    [System.Serializable]
    public class clsMTodoTickets
    {
        #region Variables
        /// <summary>
        /// <para>Texto del Ticket</para>
        /// </summary>
        public string Texto;                                                                // Texto del Ticket
        /// <summary>
        /// <para>Nota del Ticket</para>
        /// </summary>
        public string Nota;                                                                 // Nota del Ticket
        /// <summary>
        /// <para>Categoria del Ticket</para>
        /// </summary>
        public string Categoria;                                                            // Categoria del Ticket
        /// <summary>
        /// <para>Archivo del Ticket</para>
        /// </summary>
        public string Archivo;                                                              // Archivo del Ticket
        /// <summary>
        /// <para>Linea del Ticket</para>
        /// </summary>
        public int Linea;                                                                   // Linea del Ticket
        /// <summary>
        /// <para>Ruta que se tiene que mostrar</para>
        /// </summary>
        public string PathAMostrar;                                                         // Ruta que se tiene que mostrar
        #endregion

        #region Constructor
        /// <summary>
        /// <para>Mtodo Tickets</para>
        /// </summary>
        /// <param name="tex">Texto del Ticket</param>
        /// <param name="nota">Nota del Ticket</param>
        /// <param name="cat">Categoria del Ticket</param>
        /// <param name="arch">Archivo del Ticket</param>
        /// <param name="linea">Linea del Ticket</param>
        public clsMTodoTickets(string tex, string nota, string cat, string arch, int linea)// Mtodo Tickets
        {
            Texto = tex;
            Nota = nota;
            Categoria = cat;
            Archivo = arch;
            Linea = linea;

            PathAMostrar = Archivo.Remove(0, Application.dataPath.Length - 6).Replace("\\", "/") + "(" + Linea + ")";
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Equals</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)// Equals
        {
            var x = this;
            var y = (clsMTodoTickets)obj;
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return string.Equals(x.Texto, y.Texto) && string.Equals(x.Nota, y.Nota) && string.Equals(x.Categoria, y.Categoria) && string.Equals(x.Archivo, y.Archivo) && x.Linea == y.Linea;
        }

        /// <summary>
        /// <para>Obtiene el HashCode</para>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()// Obtiene el HashCode
        {
            unchecked
            {
                var obj = this;
                var hashCode = (obj.Texto != null ? obj.Texto.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Nota != null ? obj.Nota.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Categoria != null ? obj.Categoria.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Archivo != null ? obj.Archivo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.Linea;
                return hashCode;
            }
        }
        #endregion
    }
}