using UnityEngine;

public class recolectable : MonoBehaviour
{
    public int puntos = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<interfaz>().SumarPuntos(puntos);
            Destroy(gameObject);
        }
    }
}