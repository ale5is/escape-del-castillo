using UnityEngine;
using TMPro;

public class interfaz : MonoBehaviour
{
    public float tiempo = 0f;
    public int puntaje = 0;

    public TMP_Text tiempoTexto;
    public TMP_Text puntajeTexto;

    void Update()
    {
        tiempo += Time.deltaTime;
        tiempoTexto.text = "Tiempo: " + Mathf.FloorToInt(tiempo);

        puntajeTexto.text = "Puntaje: " + puntaje;
    }

    public void SumarPuntos(int puntos)
    {
        puntaje += puntos;
    }
}