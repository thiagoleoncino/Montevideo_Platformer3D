using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Monedas : MonoBehaviour
{
    public float rotationSpeed;
    public Transform targetTransform; // Transform al que se moverá la moneda
    public float moveSpeed = 20f; // Velocidad del movimiento

    private Rigidbody rb; // Para almacenar el Rigidbody

    void Start()
    {
        // Obtener el Rigidbody del objeto
        rb = GetComponent<Rigidbody>();

        // Buscar el objeto "MonedaUI" en la escena y obtener su Transform
        GameObject targetObject = GameObject.Find("MonedaUI");
        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto llamado 'MonedaUI' en la escena.");
        }
    }

    void Update()
    {
        // Rotar el objeto en el eje Y
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDetection"))
        {
            // Desactivar el Rigidbody y las colisiones
            if (rb != null)
            {
                rb.isKinematic = true; // Desactivar el movimiento del Rigidbody
            }

            // Desactivar el collider
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false; // Desactivar las colisiones
            }

            // Iniciar el movimiento hacia el target
            StartCoroutine(MoveToTarget());
        }
    }

    IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, targetTransform.position) > 0.1f)
        {
            // Mover el objeto hacia el transform objetivo
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
            yield return null; // Esperar al siguiente frame
        }

        // Destruir el objeto después de alcanzar el target
        Destroy(gameObject);
    }
}

