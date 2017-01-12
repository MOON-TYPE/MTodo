//                                  ┌∩┐(◣_◢)┌∩┐
//                                                                              \\
// clsMTodoTickets.cs (00/00/0000)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

public class clsMTodoTickets
{
    #region Variables
    public string Texto;

    public string Nota;

    public string Categoria;

    public string Archivo;

    public int Linea;

    public string PathAMostrar;
    #endregion

    public clsMTodoTickets(string tex,string nota,string cat,string arch,int linea)
    {
        Texto = tex;
        Nota = nota;
        Categoria = cat;
        Archivo = arch;
        Linea = linea;

        PathAMostrar = Archivo.Remove(0, Application.dataPath.Length - 6).Replace("\\", "/") + "(" + Linea + ")";
    }

    public override bool Equals(object obj)
    {
        var x = this;
        var y = (clsMTodoTickets)obj;
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;

        return string.Equals(x.Texto, y.Texto) && string.Equals(x.Nota, y.Nota) && string.Equals(x.Categoria, y.Categoria) && string.Equals(x.Archivo, y.Archivo) && x.Linea == y.Linea;
    }

    public override int GetHashCode()
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

}