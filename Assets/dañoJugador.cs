using UnityEngine;

public class dañoTrigger : MonoBehaviour
{
    [SerializeField] private int daño = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Vida vida = other.GetComponent<Vida>();

        if (vida != null)
            vida.RecibirDaño(daño);
    }
}