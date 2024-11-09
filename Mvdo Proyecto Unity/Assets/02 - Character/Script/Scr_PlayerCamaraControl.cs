using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCamaraControl : MonoBehaviour
{
    private Transform cameraTransform; //Transform de la Camara
    public Transform playerTransform; //Transform del Jugador

    [Header("Ajustes de rotaci�n")]
    public float rotationSpeed = 2.5f; // Velocidad de rotaci�n
    private Quaternion targetRotation; // Rotaci�n objetivo
    public float stickRotationSpeed = 90f; // Velocidad de rotaci�n con el stick

    private void Start()
    {
        cameraTransform = gameObject.transform;
        targetRotation = cameraTransform.rotation; // Inicializamos la rotaci�n objetivo como la actual
    }

    private void Update()
    {
        // Inputs para los triggers izquierdo y derecho
        bool leftTrigger = Input.GetButton("Fire1");
        bool rightTrigger = Input.GetButton("Fire2");

        // Reseteamos la rotaci�n objetivo a la del jugador
        if (leftTrigger)
        {
            targetRotation = playerTransform.rotation;
        }

        // Control del stick para rotar la c�mara
        HandleStickCameraRotation();

        // Aplicamos la rotaci�n suavemente hacia la rotaci�n objetivo
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void HandleStickCameraRotation() //Funcion para rotar la camara con el Stick.
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
