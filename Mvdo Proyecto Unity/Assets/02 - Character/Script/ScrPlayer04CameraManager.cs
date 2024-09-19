using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class ScrPlayer04CameraManager : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform playerTransform;

    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer03ActionManager playerActions;

    [Header("Comportamiento de Camara")]
    public bool isFollowingPlayer;
    public bool isResettingCameraRotation;

    [Header("Ajustes de Rotacion")]
    public float rotationSpeed;
    public float smoothTransitionSpeed;
    private float currentRotationSpeed;
    private Quaternion targetRotation;

    private void Awake()
    {
        cameraTransform = GameObject.Find("VirtulCamara").transform;
        playerTransform = GetComponentInParent<Transform>();

        playerInputs = GetComponentInParent<ScrPlayer01ControlManager>();
        playerActions = GetComponentInParent<ScrPlayer03ActionManager>();
    }

    private void Update()
    {
        if (playerInputs.InputRightShoulder1)
        {
            StartResetCamera();
        }

        if (isResettingCameraRotation)
        {
            SmoothResetCameraRotation();
        }

        if (playerActions.playerIsStanding) //NEW
        {
            if (playerActions.playerIsMoving)
            {
                HandleCameraRotation();
                isFollowingPlayer = true;
            }
            else { isFollowingPlayer = false; }
        }

    }
    private void StartResetCamera()
    {
        targetRotation = Quaternion.Euler(0f, playerTransform.eulerAngles.y, 0f);
        isResettingCameraRotation = true;
    }

    private void SmoothResetCameraRotation()
    {
        // Interpolación suave hacia la rotación objetivo
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, smoothTransitionSpeed * Time.deltaTime);

        // Finaliza la transición cuando se alcanza la rotación objetivo
        if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 0.1f)
        {
            isResettingCameraRotation = false;
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
        switch (playerInputs.stickDirection)
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

}