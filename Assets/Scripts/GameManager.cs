using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Jugador")]
    public int vidaMax = 5;
    public int vidaActual = 5;
    public int vidas = 3;

    [Header("Checkpoint")]
    public Vector3 puntoRespawn;
    public bool tieneCheckpoint = false;

    [Header("Interfaz")]
    public float tiempo = 0f;
    public int puntaje = 0;

    private int escenaActual;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            escenaActual = SceneManager.GetActiveScene().buildIndex;
            SceneManager.sceneLoaded += AlCargarEscena;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= AlCargarEscena;
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        // Si cambia de escena, borrar checkpoint
        if (escena.buildIndex != escenaActual)
        {
            tieneCheckpoint = false;
            puntoRespawn = Vector3.zero;
        }

        escenaActual = escena.buildIndex;
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

    public void GuardarCheckpoint(Vector3 punto)
    {
        tieneCheckpoint = true;
        puntoRespawn = punto;
    }

    public void GuardarInterfaz(float tiempoActual, int puntajeActual)
    {
        tiempo = tiempoActual;
        puntaje = puntajeActual;
    }

    public void RestaurarInterfaz(out float tiempoActual, out int puntajeActual)
    {
        tiempoActual = tiempo;
        puntajeActual = puntaje;
    }

    public void ReiniciarDatos()
    {
        vidaActual = vidaMax;
        vidas = 3;

        tieneCheckpoint = false;
        puntoRespawn = Vector3.zero;

        tiempo = 0f;
        puntaje = 0;
    }
}