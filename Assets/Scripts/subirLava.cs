using UnityEngine;

public class subirLava : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    [SerializeField] public float distancia = 5f;

    private Vector3 posicionInicial;
    private Vector3 posicionFinal;

    void Start()
    {
        posicionInicial = transform.position;
        posicionFinal = posicionInicial + Vector3.up * distancia;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            posicionFinal,
            velocidad * Time.deltaTime
        );
    }
}