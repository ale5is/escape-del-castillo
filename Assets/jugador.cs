using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class jugador : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    [Header("Sprite")]
    public SpriteRenderer sprite;

    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Vida")]
    public int vidaMax = 5;
    public int vidaActual;

    [Header("Vidas")]
    public int vidas = 3;
    public TMP_Text textoVidas;

    [Header("Invulnerabilidad")]
    public float tiempoInvulnerable = 1.5f;
    public bool invulnerable = false;
    public bool dañado = false;

    private float timerInvulnerabilidad = 0f;

    [Header("UI")]
    public Slider barraVida;
    public GameObject canvasGameOver;

    private bool muerto = false;
    private Vector3 puntoRespawn;

    [Header("Animaciones")]
    public Animator anim; // Animator del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaActual = vidaMax;
        puntoRespawn = transform.position;

        if (barraVida != null)
        {
            barraVida.maxValue = vidaMax;
            barraVida.value = vidaActual;
        }

        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        if (canvasGameOver != null)
            canvasGameOver.SetActive(false);
    }

    void Update()
    {
        if (muerto)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        // --- MOVIMIENTO ---
        float move = Input.GetAxisRaw("Horizontal"); // -1, 0 o 1
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // --- SALTO ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // --- ANIMACIONES Y FLIP ---
        if (anim != null)
        {
            bool moviendose = move != 0;

            if (!isGrounded) // en el aire
            {
                anim.SetBool("saltar", true);
                anim.SetBool("correr", false);
                anim.SetBool("quieto", false);
            }
            else if (moviendose) // caminando
            {
                anim.SetBool("saltar", false);
                anim.SetBool("correr", true);
                anim.SetBool("quieto", false);
            }
            else // quieto
            {
                anim.SetBool("saltar", false);
                anim.SetBool("correr", false);
                anim.SetBool("quieto", true);
            }

            // --- FLIP DEL PERSONAJE ---
            if (move > 0)
                sprite.flipX = false; // mirando a la derecha
            else if (move < 0)
                sprite.flipX = true;  // mirando a la izquierda
        }

        // --- INVULNERABILIDAD ---
        if (invulnerable)
        {
            timerInvulnerabilidad -= Time.deltaTime;
            if (timerInvulnerabilidad <= 0)
            {
                invulnerable = false;
                timerInvulnerabilidad = 0;
            }
        }

        // --- DAÑO CONTINUO ---
        if (dañado && !invulnerable)
        {
            AplicarDaño(1);
            invulnerable = true;
            timerInvulnerabilidad = tiempoInvulnerable;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("saltar", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn"))
        {
            float baseY = other.bounds.min.y;
            puntoRespawn = new Vector3(other.transform.position.x, baseY, transform.position.z);
        }
    }

    public void RecibirDaño(int daño)
    {
        if (invulnerable || muerto) return;

        AplicarDaño(daño);
        invulnerable = true;
        timerInvulnerabilidad = tiempoInvulnerable;
    }

    void AplicarDaño(int daño)
    {
        vidaActual -= daño;

        if (barraVida != null)
            barraVida.value = vidaActual;

        if (vidaActual <= 0)
            PerderVida();
    }

    void PerderVida()
    {
        vidas--;
        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        if (vidas <= 0)
            Morir();
        else
            Respawn();
    }

    void Respawn()
    {
        vidaActual = vidaMax;
        if (barraVida != null)
            barraVida.value = vidaActual;

        transform.position = puntoRespawn;
        rb.velocity = Vector2.zero;
    }

    void Morir()
    {
        muerto = true;
        if (canvasGameOver != null)
            canvasGameOver.SetActive(true);

        rb.velocity = Vector2.zero;
    }
}