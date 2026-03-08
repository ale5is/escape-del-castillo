using UnityEngine;

public class llaves : MonoBehaviour
{
    [Header("Nombre del objeto que se guardará en el inventario")]
    public string nombreObjeto; // Lo asignás en el Inspector

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            JugadorInventario inv = collision.GetComponent<JugadorInventario>();
            if (inv != null && !string.IsNullOrEmpty(nombreObjeto))
            {
                inv.AgregarObjeto(nombreObjeto);
                Debug.Log("Objeto recogido: " + nombreObjeto);
            }

            Destroy(gameObject); // Se destruye el objeto de la escena
        }
    }
}