using UnityEngine;

public class enemigo : MonoBehaviour
{
    public float velocidad = 2f;
    public Transform puntoIzquierda;
    public Transform puntoDerecha;

    public int daño = 1;

    private bool moviendoDerecha = true;

    void Update()
    {
        if (moviendoDerecha)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                puntoDerecha.position,
                velocidad * Time.deltaTime
            );

            if (transform.position.x >= puntoDerecha.position.x)
            {
                moviendoDerecha = false;
                Girar();
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                puntoIzquierda.position,
                velocidad * Time.deltaTime
            );

            if (transform.position.x <= puntoIzquierda.position.x)
            {
                moviendoDerecha = true;
                Girar();
            }
        }
    }

    void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugador j = other.GetComponent<jugador>();

            if (j != null)
            {
                j.RecibirDaño(daño);
            }
        }
    }
}