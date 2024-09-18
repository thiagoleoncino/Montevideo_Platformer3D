using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayer05MovementManager : MonoBehaviour
{
    // Referencias a otros scripts
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer04StatsManager playerStats;
    private ScrPlayer03ActionManager playerActions;

    [HideInInspector] public Rigidbody rigidBody;
    private Transform cameraTransform;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        // Inicializar componentes
        rigidBody = GetComponent<Rigidbody>();
        cameraTransform = GameObject.Find("VirtulCamara").transform;

        //Scripts
        playerInputs = GetComponent<ScrPlayer01ControlManager>();
        playerStats = GetComponent<ScrPlayer04StatsManager>();
        playerActions = GetComponent<ScrPlayer03ActionManager>();
    }

    private void FixedUpdate()
    {
        if (playerActions.playerCanMove)
        {
            HandleHorizontalMovement();
        } else { currentVelocity = Vector3.zero; }

    }

    private void HandleHorizontalMovement()
    {
        Vector2 stickInput = playerInputs.stickInput;

        if (stickInput == Vector2.zero)
        {
            ApplyDeceleration();
            return;
        }
        
        Vector3 moveDirection = CalculateMoveDirection(stickInput);

        float magnitude = stickInput.magnitude;
        float targetSpeed = Mathf.Lerp(0, playerStats.walkSpeed, magnitude);

        currentVelocity = Vector3.Lerp(currentVelocity, moveDirection * targetSpeed, Time.fixedDeltaTime * playerStats.acceleration);

        Vector3 movement = currentVelocity * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);

        RotateTowardsMovementDirection(moveDirection);
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
            Quaternion newRotation = Quaternion.Slerp(rigidBody.rotation, targetRotation, Time.fixedDeltaTime * playerStats.turnResistance);
            rigidBody.MoveRotation(newRotation);
        }
    }

    private void ApplyDeceleration()
    {
        currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, Time.fixedDeltaTime * playerStats.deceleration);
        Vector3 movement = currentVelocity * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);
    }
}