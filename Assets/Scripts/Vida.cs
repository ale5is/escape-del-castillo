using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Vida : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidaMax = 5;
    [SerializeField] private int vidas = 3;

    [Header("Invulnerabilidad")]
    [SerializeField] private float tiempoInvulnerable = 1.5f;

    [Header("UI")]
    [SerializeField] private Slider barraVida;
    [SerializeField] private TMP_Text textoVidas;
    [SerializeField] private GameObject canvasGameOver;

    private int vidaActual;
    private bool invulnerable;
    private bool muerto;
    private float timerInvulnerabilidad;

    [Header("Respawn")]
    private Vector3 puntoRespawn;
    private bool tieneCheckpoint = false;

    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        // Cargar vidas guardadas
        if (GameManager.Instance != null)
        {
            vidas = GameManager.Instance.vidas;
            vidaMax = GameManager.Instance.vidaMax;
        }

        // Siempre empieza con vida completa
        vidaActual = vidaMax;

        // Punto inicial de respawn
        puntoRespawn = transform.position;

        ActualizarUI();

        if (canvasGameOver != null)
            canvasGameOver.SetActive(false);
    }


    void Update()
    {
        // Control de invulnerabilidad
        if (invulnerable)
        {
            timerInvulnerabilidad -= Time.deltaTime;

            if (timerInvulnerabilidad <= 0)
                invulnerable = false;
        }


        // Reiniciar después de Game Over
        if (muerto && Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.ReiniciarDatos();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    public void RecibirDaño(int daño)
    {
        if (muerto || invulnerable)
            return;


        vidaActual = Mathf.Max(0, vidaActual - daño);

        ActualizarUI();


        if (vidaActual <= 0)
            PerderVida();


        invulnerable = true;
        timerInvulnerabilidad = tiempoInvulnerable;
    }


    void PerderVida()
    {
        vidas--;

        GuardarDatos();
        ActualizarUI();


        if (vidas <= 0)
        {
            Morir();
            return;
        }


        // Si nunca tocó checkpoint
        if (!tieneCheckpoint)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }


        Respawn();
    }


    void Respawn()
    {
        // Recupera toda la vida
        vidaActual = vidaMax;

        transform.position = puntoRespawn;


        if (rb != null)
            rb.velocity = Vector2.zero;


        ActualizarUI();
    }


    void Morir()
    {
        muerto = true;


        if (rb != null)
            rb.velocity = Vector2.zero;


        if (canvasGameOver != null)
            canvasGameOver.SetActive(true);
    }


    void GuardarDatos()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GuardarVida(
                vidaActual,
                vidas
            );
        }
    }


    void ActualizarUI()
    {
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMax;
            barraVida.value = vidaActual;
        }


        if (textoVidas != null)
            textoVidas.text = $"Vidas: {vidas}";
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn"))
        {
            tieneCheckpoint = true;


            puntoRespawn = new Vector3(
                other.transform.position.x,
                other.bounds.min.y,
                transform.position.z
            );


            Debug.Log("Checkpoint guardado: " + puntoRespawn);
        }
    }


    public int ObtenerVida()
    {
        return vidaActual;
    }


    public int ObtenerVidas()
    {
        return vidas;
    }
}