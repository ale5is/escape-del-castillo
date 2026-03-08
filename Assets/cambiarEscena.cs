using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiarEscena : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string nombreEscena;

    [Header("Tecla para cambiar de escena")]
    public KeyCode tecla = KeyCode.Space; // Por defecto usa la barra espaciadora

    void Update()
    {
        // Detecta cuando se presiona la tecla
        if (Input.GetKeyDown(tecla))
        {
            if (!string.IsNullOrEmpty(nombreEscena))
            {
                SceneManager.LoadScene(nombreEscena);
            }
            else
            {
                Debug.LogWarning("No se asignó ninguna escena a cargar");
            }
        }
    }
}