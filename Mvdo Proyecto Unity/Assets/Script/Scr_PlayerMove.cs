using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerMove : MonoBehaviour
{
    [Header("Estado")]
    public bool canMove = true;
    public bool isRunning = false;

    [Header("Inputs")]
    public InputActionAsset inputActions;
    public Vector2 stickInput;
    public string stickDirection;
    public float stickThreshold = 0.5f;

    [Header("Movimiento")]
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public Rigidbody rigidBody;
    private Transform cameraTransform;

    private void Awake()
    {
        // Inicializar componentes
        rigidBody = GetComponent<Rigidbody>();
        var actionMap = inputActions.FindActionMap("NormalActions");
        moveAction = actionMap.FindAction("Movement");
        cameraTransform = GameObject.Find("VirtulCamara").transform;
    }

    private void Start()
    {
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        stickInput = moveAction.ReadValue<Vector2>();

        if (stickInput == Vector2.zero)
        {
            isRunning = false;
            return;
        }

        Vector3 moveDirection = CalculateMoveDirection(stickInput);

        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 movement = moveDirection * speed * Time.fixedDeltaTime;

        rigidBody.MovePosition(rigidBody.position + movement);

        RotateTowardsMovementDirection(moveDirection);

        DetermineStickState(stickInput);
    }

    private Vector3 CalculateMoveDirection(Vector2 stickInput)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        return (stickInput.x * right + stickInput.y * forward).normalized;
    }

    private void RotateTowardsMovementDirection(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }
    }

    private void DetermineStickState(Vector2 stickInput)
    {
        float angle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        float magnitude = stickInput.magnitude;

        // Determinar dirección del stick
        string direction = GetStickDirection(angle);

        // Determinar zona del stick
        string zone = magnitude < stickThreshold ? "Círculo Interno" : "Círculo Externo";
        isRunning = magnitude >= stickThreshold;

        Debug.Log($"Dirección del stick: {direction} en {zone}");
    }

    public string GetStickDirection(float angle)
    {
        if (angle >= 67.5f && angle < 112.5f)
            stickDirection = "Arriba";
        else if (angle >= 112.5f && angle < 157.5f)
            stickDirection = "Izquierda Diagonal Arriba";
        else if (angle >= 157.5f && angle < 202.5f)
            stickDirection = "Izquierda";
        else if (angle >= 202.5f && angle < 247.5f)
            stickDirection = "Izquierda Diagonal Abajo";
        else if (angle >= 247.5f && angle < 292.5f)
            stickDirection = "Abajo";
        else if (angle >= 292.5f && angle < 337.5f)
            stickDirection = "Derecha Diagonal Abajo";
        else if (angle >= 337.5f || angle < 22.5f)
            stickDirection = "Derecha";
        else if (angle >= 22.5f && angle < 67.5f)
            stickDirection = "Derecha Diagonal Arriba";

        return stickDirection;
    }
}
