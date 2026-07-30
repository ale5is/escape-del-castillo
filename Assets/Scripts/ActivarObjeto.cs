using UnityEngine;

public class ActivarObjeto : MonoBehaviour
{
    public GameObject objetoAActivar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objetoAActivar.SetActive(true);
        }
    }
}