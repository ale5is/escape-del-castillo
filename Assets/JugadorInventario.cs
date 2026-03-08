using System.Collections.Generic;
using UnityEngine;

public class JugadorInventario : MonoBehaviour
{
    public List<string> objetos = new List<string>();

    // Agregar objeto al inventario
    public void AgregarObjeto(string objeto)
    {
        if (!objetos.Contains(objeto))
            objetos.Add(objeto);
    }

    // Revisar si tiene todos los objetos necesarios
    public bool TieneObjetos(List<string> requeridos)
    {
        foreach (string obj in requeridos)
        {
            if (!objetos.Contains(obj))
                return false;
        }
        return true;
    }
}