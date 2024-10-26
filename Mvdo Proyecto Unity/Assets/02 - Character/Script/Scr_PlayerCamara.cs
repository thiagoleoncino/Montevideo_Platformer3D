using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCamara : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerTransform;
    //public float rotationAngle = 45f; // Ángulo de rotación por segundo
    public float rotationSpeed = 5f;  // Velocidad de rotación suave
    private Quaternion targetRotation; // Rotación objetivo

    [Header("Ajustes de rotación de stick")]
    public float stickRotationSpeed = 60f;  // Velocidad de rotación con el stick
                                            // public float smoothTransitionSpeed = 5f; // Velocidad de suavizado

    private void Start()
    {
        targetRotation = cameraTransform.rotation; // Inicializamos la rotación objetivo como la actual
    }

    private void Update()
    {
        // Inputs para los triggers izquierdo y derecho (mantener presionados)
        bool leftTrigger = Input.GetButton("Fire1");
        bool rightTrigger = Input.GetButton("Fire2");

        // Rotación de la cámara con triggers
        if (leftTrigger)
        {
            targetRotation = playerTransform.rotation; // Reseteamos la rotación objetivo a la del jugador
        }

        if (rightTrigger)
        {
            //SRotateCameraSmoothly(rotationAngle * Time.deltaTime); // Rota gradualmente a la derecha
        }

        // Control del stick para rotar la cámara
        HandleStickCameraRotation();

        // Aplicamos la rotación suavemente hacia la rotación objetivo
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void RotateCameraSmoothly(float angle)
    {
        // Calcula la nueva rotación objetivo sumando el ángulo especificado
        targetRotation *= Quaternion.Euler(0f, angle, 0f);
    }

    private void HandleStickCameraRotation()
    {
        // Obtén la entrada del stick en el eje horizontal (ej. X para rotación izquierda-derecha)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Si hay entrada horizontal (stick movido a izquierda o derecha)
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Calcula la rotación basada en la entrada del stick
            float rotationAmount = horizontalInput * stickRotationSpeed * Time.deltaTime;

            // Actualiza la rotación objetivo de la cámara
            targetRotation *= Quaternion.Euler(0f, rotationAmount, 0f);
        }
    }
}
