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
    #endregion

    public clsMTodoTickets(string tex,string nota,string cat,string arch,int linea)
    {
        Texto = tex;
        Nota = nota;
        Categoria = cat;
        Archivo = arch;
        Linea = linea;
    }

}