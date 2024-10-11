using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Prueba : MonoBehaviour
{
    public Transform cameraTransform;
    public float rotationAngle = 45f; // Ángulo de rotación por segundo
    public float rotationSpeed = 5f;  // Velocidad de rotación suave
    private Quaternion targetRotation; // Rotación objetivo

    private void Start()
    {
        targetRotation = cameraTransform.rotation; // Inicializamos la rotación objetivo como la actual
    }

    private void Update()
    {
        // Inputs para los triggers izquierdo y derecho (mantener presionados)
        bool leftTrigger = Input.GetButton("Fire1");
        bool rightTrigger = Input.GetButton("Fire2");

        if (leftTrigger)
        {
            RotateCameraSmoothly(-rotationAngle * Time.deltaTime); // Rota gradualmente a la izquierda
        }

        if (rightTrigger)
        {
            RotateCameraSmoothly(rotationAngle * Time.deltaTime); // Rota gradualmente a la derecha
        }

        // Aplicamos la rotación suavemente hacia la rotación objetivo
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void RotateCameraSmoothly(float angle)
    {
        // Calcula la nueva rotación objetivo sumando el ángulo especificado
        targetRotation *= Quaternion.Euler(0f, angle, 0f);
    }
}
