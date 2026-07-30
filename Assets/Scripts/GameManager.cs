using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Jugador")]
    public int vidaMax = 5;
    public int vidaActual = 5;
    public int vidas = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GuardarVida(int vida, int vidasActuales)
    {
        vidaActual = vida;
        vidas = vidasActuales;
    }

    public void RestaurarValores(out int vida, out int vidasActuales)
    {
        vida = vidaActual;
        vidasActuales = vidas;
    }

    public void ReiniciarDatos()
    {
        vidaActual = vidaMax;
        vidas = 3;
    }
}