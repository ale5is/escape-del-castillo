using UnityEngine;
using System.Collections;

public class dañoTrigger : MonoBehaviour
{
    public int daño = 1;
    public float tiempoEntreDaño = 1.5f;

    public bool puedeHacerDaño = true;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && puedeHacerDaño)
        {
            jugador j = other.GetComponent<jugador>();

            if (j != null)
            {
                j.dañado=true;
                
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && puedeHacerDaño)
        {
            jugador j = other.GetComponent<jugador>();

            if (j != null)
            {
                j.dañado = false;

            }
        }
    }

}