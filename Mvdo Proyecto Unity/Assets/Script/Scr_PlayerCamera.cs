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
    public float smoothTransitionSpeed = 2f; // Velocidad de la transición suave

    private InputAction rightTrigger;
    private bool isResettingCamera = false; // Variable para saber si estamos en la transición
    private Quaternion targetRotation; // La rotación objetivo

    private void Awake()
    {
        scriptMove = GetComponentInParent<Scr_PlayerMove>();
        var actionMap = scriptMove.inputActions.FindActionMap("NormalActions");
        rightTrigger = actionMap.FindAction("ResetCamera");

        // Suscribirse al evento para detectar cuando se presiona el botón
        rightTrigger.performed += ctx => StartResetCameraRotation();
    }

    private void Update()
    {
        Vector2 stickInput = scriptMove.moveAction.ReadValue<Vector2>();

        if (isResettingCamera)
        {
            // Interpolación suave hacia la rotación objetivo
            camaraTransform.rotation = Quaternion.Slerp(camaraTransform.rotation, targetRotation, smoothTransitionSpeed * Time.deltaTime);

            // Comprobar si la rotación ha llegado a la rotación objetivo
            if (Quaternion.Angle(camaraTransform.rotation, targetRotation) < 0.1f)
            {
                isResettingCamera = false; // Finaliza la transición
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
        // Obtener la rotación actual de la cámara
        Quaternion currentRotation = camaraTransform.rotation;

        // Calcular la nueva rotación aplicando el giro en el eje Y
        Quaternion newRotation = Quaternion.Euler(0f, currentRotation.eulerAngles.y + rotationAmount, 0f);

        // Aplicar la nueva rotación
        camaraTransform.rotation = newRotation;
    }

    private void StartResetCameraRotation()
    {
        // Calcular la rotación objetivo basada en la rotación del playerTransform
        targetRotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y, 0f);
        isResettingCamera = true; // Iniciar la transición suave
    }
}
