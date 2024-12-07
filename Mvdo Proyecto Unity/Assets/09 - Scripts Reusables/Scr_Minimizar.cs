using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Minimizar : MonoBehaviour
{
    public float shrinkSpeed = 0.1f;  // Velocidad de reducción de escala
    public float lifetime = 5f;       // Tiempo en segundos antes de destruir el objeto

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;  // Guarda la escala original del objeto
        StartCoroutine(Shrink());  // Inicia la reducción de escala
        Destroy(gameObject, lifetime);  // Destruye el objeto después de un tiempo
    }

    // Coroutine para reducir la escala gradualmente
    private IEnumerator Shrink()
    {
        float startTime = Time.time;

        while (Time.time - startTime < lifetime)
        {
            // Calcula el factor de reducción basado en el tiempo
            float scaleFactor = 1 - (Time.time - startTime) / lifetime;
            transform.localScale = originalScale * scaleFactor;
            yield return null;  // Espera al siguiente frame
        }

        // Asegura que el objeto tenga una escala de 0 antes de destruirlo
        transform.localScale = Vector3.zero;
    }
}
