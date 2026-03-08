using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class Puerta : MonoBehaviour
{
    [Header("Objetos que necesita el jugador para abrir la puerta")]
    public List<string> objetosNecesarios = new List<string>();
    [Header("Nombre de la escena a cargar")]
    public string nombreEscenaSiguiente; // Nombre exacto de la escena

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        JugadorInventario inv = collision.GetComponent<JugadorInventario>();
        if (inv == null) return;

        List<string> faltantes = new List<string>();

        foreach (string obj in objetosNecesarios)
        {
            if (!inv.objetos.Contains(obj.Trim().ToLower()))
            {
                faltantes.Add(obj);
            }
        }

        if (faltantes.Count == 0)
        {
            // Cambia de escena directamente
            if (!string.IsNullOrEmpty(nombreEscenaSiguiente))
                SceneManager.LoadScene(nombreEscenaSiguiente);
            else
                Debug.LogWarning("No se asignó ninguna escena a cargar");
        }
        else
        {
            string mensaje = "Te faltan los objetos: " + string.Join(", ", faltantes);
            Debug.Log(mensaje);
        }
    }
}