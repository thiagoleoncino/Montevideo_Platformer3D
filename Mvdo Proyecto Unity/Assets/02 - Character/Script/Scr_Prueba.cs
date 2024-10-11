using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Prueba : MonoBehaviour
{
    public Transform cameraTransform;
    public float rotationAngle = 45f; // �ngulo de rotaci�n por segundo
    public float rotationSpeed = 5f;  // Velocidad de rotaci�n suave
    private Quaternion targetRotation; // Rotaci�n objetivo

    private void Start()
    {
        targetRotation = cameraTransform.rotation; // Inicializamos la rotaci�n objetivo como la actual
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

        // Aplicamos la rotaci�n suavemente hacia la rotaci�n objetivo
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void RotateCameraSmoothly(float angle)
    {
        // Calcula la nueva rotaci�n objetivo sumando el �ngulo especificado
        targetRotation *= Quaternion.Euler(0f, angle, 0f);
    }
}
