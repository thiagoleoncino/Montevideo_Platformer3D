using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerCamera : MonoBehaviour
{
    private Scr_PlayerMove scriptMove;
    public Transform camaraTransform;
    public Transform playerTransform;

    public float rotationSpeed = 100f;
    public float smoothTransitionSpeed = 2f; // Velocidad de la transici�n suave

    private InputAction rightTrigger;
    private bool isResettingCamera = false; // Variable para saber si estamos en la transici�n
    private Quaternion targetRotation; // La rotaci�n objetivo

    private void Awake()
    {
        scriptMove = GetComponentInParent<Scr_PlayerMove>();
        var actionMap = scriptMove.inputActions.FindActionMap("NormalActions");
        rightTrigger = actionMap.FindAction("ResetCamera");

        // Suscribirse al evento para detectar cuando se presiona el bot�n
        rightTrigger.performed += ctx => StartResetCameraRotation();
    }

    private void Update()
    {
        Vector2 stickInput = scriptMove.moveAction.ReadValue<Vector2>();

        if (isResettingCamera)
        {
            // Interpolaci�n suave hacia la rotaci�n objetivo
            camaraTransform.rotation = Quaternion.Slerp(camaraTransform.rotation, targetRotation, smoothTransitionSpeed * Time.deltaTime);

            // Comprobar si la rotaci�n ha llegado a la rotaci�n objetivo
            if (Quaternion.Angle(camaraTransform.rotation, targetRotation) < 0.1f)
            {
                isResettingCamera = false; // Finaliza la transici�n
            }
        }
        else
        {
            float rotationAmount = 0f;

            if (scriptMove.isRunning)
            {
                if (scriptMove.stickDirection == "Izquierda")
                {
                    rotationAmount = -rotationSpeed * Time.deltaTime;
                }
                if (scriptMove.stickDirection == "Izquierda Diagonal Arriba" || scriptMove.stickDirection == "Izquierda Diagonal Abajo")
                {
                    rotationAmount = (-rotationSpeed * Time.deltaTime) / 2;
                }

                if (scriptMove.stickDirection == "Derecha")
                {
                    rotationAmount = rotationSpeed * Time.deltaTime;
                }
                if (scriptMove.stickDirection == "Derecha Diagonal Arriba" || scriptMove.stickDirection == "Derecha Diagonal Abajo")
                {
                    rotationAmount = (rotationSpeed * Time.deltaTime) / 2;
                }

                if (rotationAmount != 0f)
                {
                    RotateCamera(rotationAmount);
                }
            }
        }
    }

    private void RotateCamera(float rotationAmount)
    {
        // Obtener la rotaci�n actual de la c�mara
        Quaternion currentRotation = camaraTransform.rotation;

        // Calcular la nueva rotaci�n aplicando el giro en el eje Y
        Quaternion newRotation = Quaternion.Euler(0f, currentRotation.eulerAngles.y + rotationAmount, 0f);

        // Aplicar la nueva rotaci�n
        camaraTransform.rotation = newRotation;
    }

    private void StartResetCameraRotation()
    {
        // Calcular la rotaci�n objetivo basada en la rotaci�n del playerTransform
        targetRotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y, 0f);
        isResettingCamera = true; // Iniciar la transici�n suave
    }
}
