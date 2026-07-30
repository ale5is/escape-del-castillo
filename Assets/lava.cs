using UnityEngine;

public class lava : MonoBehaviour
{
    [SerializeField] private int daño = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Vida j = collision.gameObject.GetComponent<Vida>();

        if (j != null)
            j.RecibirDaño(daño);
    }
}