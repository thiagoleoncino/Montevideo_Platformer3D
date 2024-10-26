using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCamara : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    //public float rotationAngle = 45f; // �ngulo de rotaci�n por segundo
    public float rotationSpeed = 5f;  // Velocidad de rotaci�n suave
    private Quaternion targetRotation; // Rotaci�n objetivo

    [Header("Ajustes de rotaci�n de stick")]
    public float stickRotationSpeed = 60f;  // Velocidad de rotaci�n con el stick
                                            // public float smoothTransitionSpeed = 5f; // Velocidad de suavizado

    private void Start()
    {
        targetRotation = cameraTransform.rotation; // Inicializamos la rotaci�n objetivo como la actual
    }

    private void Update()
    {
        // Inputs para los triggers izquierdo y derecho (mantener presionados)
        bool leftTrigger = Input.GetButton("Fire1");
        bool rightTrigger = Input.GetButton("Fire2");

        // Rotaci�n de la c�mara con triggers
        if (leftTrigger)
        {
            targetRotation = playerTransform.rotation; // Reseteamos la rotaci�n objetivo a la del jugador
        }

        if (rightTrigger)
        {
            //SRotateCameraSmoothly(rotationAngle * Time.deltaTime); // Rota gradualmente a la derecha
        }

        // Control del stick para rotar la c�mara
        HandleStickCameraRotation();

        // Aplicamos la rotaci�n suavemente hacia la rotaci�n objetivo
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void RotateCameraSmoothly(float angle)
    {
        // Calcula la nueva rotaci�n objetivo sumando el �ngulo especificado
        targetRotation *= Quaternion.Euler(0f, angle, 0f);
    }

    private void HandleStickCameraRotation()
    {
        // Obt�n la entrada del stick en el eje horizontal (ej. X para rotaci�n izquierda-derecha)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Si hay entrada horizontal (stick movido a izquierda o derecha)
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Calcula la rotaci�n basada en la entrada del stick
            float rotationAmount = horizontalInput * stickRotationSpeed * Time.deltaTime;

            // Actualiza la rotaci�n objetivo de la c�mara
            targetRotation *= Quaternion.Euler(0f, rotationAmount, 0f);
        }
    }
}
