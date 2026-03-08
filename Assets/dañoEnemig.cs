using UnityEngine;

public class dañoEnemigo : MonoBehaviour
{
    public GameObject objetoADestruir;
    public float fuerzaRebote = 8f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pdaño"))
        {
            // Rebote del jugador
            Rigidbody2D rb = other.GetComponentInParent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, fuerzaRebote);
            }

            // Destruir objeto extra
            if (objetoADestruir != null)
            {
                Destroy(objetoADestruir);
            }

            // Destruir enemigo
            Destroy(gameObject);
        }
    }
}