using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Crate_Effect : MonoBehaviour
{
    public float explosionForce = 5f; // Fuerza de la explosión
    public float despawnTime = 5f; // Tiempo antes de que los objetos desaparezcan

    void Start()
    {
        // Obtener todos los hijos del objeto
        foreach (Transform child in transform)
        {
            // Hacer que el objeto hijo sea independiente (desasociar del padre)
            child.parent = null;

            // Agregar un Rigidbody si no tiene uno
            Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = child.gameObject.AddComponent<Rigidbody>();
            }

            // Generar una dirección aleatoria, solo en el eje Y (hacia arriba) y en el eje Z (hacia adelante)
            Vector3 randomDirection = new Vector3(
                0f, // Componente horizontal aleatorio (X) es 0 porque no queremos movimiento lateral
                Random.Range(0.5f, 1f), // Componente vertical aleatorio (Y) hacia arriba
                Random.Range(0.5f, 1f)  // Componente horizontal aleatorio (Z) hacia adelante (no negativo)
            ).normalized;

            // Aplicar una fuerza explosiva en esa dirección
            rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);

            // Destruir el objeto después de cierto tiempo
            Destroy(child.gameObject, despawnTime);
        }

        // Destruir el contenedor (este objeto) si no lo necesitas más
        Destroy(gameObject);
    }
}

