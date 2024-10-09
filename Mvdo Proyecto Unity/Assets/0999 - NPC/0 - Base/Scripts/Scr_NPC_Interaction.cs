using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_NPC_Interaction : MonoBehaviour
{
    private float rotationSpeed = 100f; // Velocidad de rotación en grados por segundo
    private MeshRenderer meshRenderer;  // Cambié el nombre a "meshRenderer" para evitar confusión con el tipo de dato

    void Start()
    {
        // Asigna el MeshRenderer del objeto
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    void Update()
    {
        // Rotar el objeto en el eje Y
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que ha entrado en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            if (meshRenderer != null)
            {
                // Hacer que el MeshRenderer sea visible
                meshRenderer.enabled = true;
            }
            //Debug.Log("El Player ha entrado en el trigger");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que ha salido del trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            if (meshRenderer != null)
            {
                // Hacer que el MeshRenderer sea invisible
                meshRenderer.enabled = false;
            }
           // Debug.Log("El Player ha salido del trigger");
        }
    }
}

