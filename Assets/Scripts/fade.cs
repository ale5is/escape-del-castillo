using UnityEngine;
using TMPro;
using System.Collections;

public class fade : MonoBehaviour
{
    [SerializeField] private float duracionFade = 3f;

    private TMP_Text texto;
    private Color colorOriginal;

    void Start()
    {
        texto = GetComponent<TMP_Text>();

        if (texto != null)
        {
            colorOriginal = texto.color;
            StartCoroutine(HacerFade());
        }
    }

    IEnumerator HacerFade()
    {
        float tiempo = 0f;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, tiempo / duracionFade);

            texto.color = new Color(
                colorOriginal.r,
                colorOriginal.g,
                colorOriginal.b,
                alpha
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}