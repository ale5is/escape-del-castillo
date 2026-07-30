using UnityEngine;
using TMPro;

public class interfaz : MonoBehaviour
{
    public float tiempo = 0f;
    public int puntaje = 0;

    public TMP_Text tiempoTexto;
    public TMP_Text puntajeTexto;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestaurarInterfaz(out tiempo, out puntaje);
        }

        ActualizarUI();
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GuardarInterfaz(tiempo, puntaje);
        }

        ActualizarUI();
    }

    public void SumarPuntos(int puntos)
    {
        puntaje += puntos;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GuardarInterfaz(tiempo, puntaje);
        }

        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (tiempoTexto != null)
            tiempoTexto.text = "Tiempo: " + Mathf.FloorToInt(tiempo);

        if (puntajeTexto != null)
            puntajeTexto.text = "Puntaje: " + puntaje;
    }
}