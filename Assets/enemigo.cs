using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private Transform puntoIzquierda;
    [SerializeField] private Transform puntoDerecha;

    [Header("Daño")]
    [SerializeField] private int daño = 1;
    [SerializeField] private float tiempoEntreGolpes = 1f;

    [Header("Componentes")]
    [SerializeField] private SpriteRenderer sprite;

    private bool moviendoDerecha = true;
    private Vida jugadorEnContacto;
    private float timerDaño;

    private void Awake()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (puntoIzquierda == null || puntoDerecha == null)
        {
            Debug.LogError($"{name}: Faltan los puntos de patrulla.");
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        Patrullar();
    }

    private void Update()
    {
        if (jugadorEnContacto == null)
            return;

        timerDaño -= Time.deltaTime;

        if (timerDaño <= 0f)
        {
            jugadorEnContacto.RecibirDaño(daño);
            timerDaño = tiempoEntreGolpes;
        }
    }

    private void Patrullar()
    {
        Transform destino = moviendoDerecha ? puntoDerecha : puntoIzquierda;

        transform.position = Vector2.MoveTowards(
            transform.position,
            destino.position,
            velocidad * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, destino.position) < 0.05f)
        {
            moviendoDerecha = !moviendoDerecha;
            Girar();
        }
    }

    private void Girar()
    {
        if (sprite != null)
            sprite.flipX = !sprite.flipX;
        else
        {
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        jugadorEnContacto = other.GetComponent<Vida>();

        if (jugadorEnContacto != null)
        {
            jugadorEnContacto.RecibirDaño(daño);
            timerDaño = tiempoEntreGolpes;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (other.GetComponent<Vida>() == jugadorEnContacto)
            jugadorEnContacto = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoIzquierda != null && puntoDerecha != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(puntoIzquierda.position, puntoDerecha.position);
            Gizmos.DrawSphere(puntoIzquierda.position, 0.1f);
            Gizmos.DrawSphere(puntoDerecha.position, 0.1f);
        }
    }
}