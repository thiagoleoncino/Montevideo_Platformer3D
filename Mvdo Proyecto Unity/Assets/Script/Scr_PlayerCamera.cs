using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform playerTransform;
    private Scr_PlayerMove scriptMove;
    private Scr_PlayerInputs scriptInputs;

    [Header("Ajustes de Rotacion de Camara")]
    public float rotationSpeed = 100f;
    public float smoothTransitionSpeed = 2f;

    private float currentRotationSpeed = 0f;
    private InputAction rightTrigger;
    private bool isResettingCamera = false;
    private Quaternion targetRotation;

    private void Awake()
    {
        cameraTransform = GameObject.Find("VirtulCamara").transform;
        playerTransform = GetComponentInParent<Transform>();
        scriptMove = GetComponentInParent<Scr_PlayerMove>();
        scriptInputs = GetComponentInParent<Scr_PlayerInputs>();

        var actionMap = scriptInputs.inputActions.FindActionMap("NormalActions");
        rightTrigger = actionMap.FindAction("ResetCamera");

        // Suscribirse al evento de reseteo de cámara
        rightTrigger.performed += _ => StartResetCameraRotation();
    }

    private void Update()
    {
        if (isResettingCamera)
        {
            SmoothResetCameraRotation();
        }
        else if (scriptMove.isRunning)
        {
            HandleCameraRotation();
        }
    }

    private void SmoothResetCameraRotation()
    {
        // Interpolación suave hacia la rotación objetivo
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, smoothTransitionSpeed * Time.deltaTime);

        // Finaliza la transición cuando se alcanza la rotación objetivo
        if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 0.1f)
        {
            isResettingCamera = false;
        }
    }

    private void HandleCameraRotation()
    {
        float targetRotationSpeed = DetermineTargetRotationSpeed();
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, targetRotationSpeed, smoothTransitionSpeed * Time.deltaTime);

        if (!Mathf.Approximately(currentRotationSpeed, 0f))
        {
            RotateCamera(currentRotationSpeed * Time.deltaTime);
        }
    }

    private float DetermineTargetRotationSpeed()
    {
        switch (scriptInputs.stickDirection)
        {
            case "Izquierda":
                return -rotationSpeed;
            case "Izquierda Diagonal Arriba":
            case "Izquierda Diagonal Abajo":
                return -rotationSpeed / 2;
            case "Derecha":
                return rotationSpeed;
            case "Derecha Diagonal Arriba":
            case "Derecha Diagonal Abajo":
                return rotationSpeed / 2;
            default:
                return 0f;
        }
    }

    private void RotateCamera(float rotationAmount)
    {
        cameraTransform.rotation *= Quaternion.Euler(0f, rotationAmount, 0f);
    }

    private void StartResetCameraRotation()
    {
        targetRotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y, 0f);
        isResettingCamera = true;
    }
}
