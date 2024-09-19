using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayer06MovementManager : MonoBehaviour
{
    // Referencias a otros scripts
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer03ActionManager playerActions;
    private ScrPlayer05StatsManager playerStats;


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
        playerStats = GetComponent<ScrPlayer05StatsManager>();
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
            if (!playerActions.playerIsCrouching)
            {
                ApplyDeceleration();
            }
            return;
        }

        Vector3 moveDirection = CalculateMoveDirection(stickInput);
        float targetSpeed;

        if (playerActions.playerIsCrouching) // Si el personaje está agachado, usar crouchSpeed y no aplicara aceleración/desaceleración
        {
            targetSpeed = playerStats.crouchSpeed;
            currentVelocity = moveDirection * targetSpeed;
        }

        if (playerActions.playerIsStanding) // Si el personaje está parado, usar walkSpeed y si aplicara aceleración/desaceleración
        {
            float magnitude = stickInput.magnitude;
            targetSpeed = Mathf.Lerp(0, playerStats.walkSpeed, magnitude);
            currentVelocity = Vector3.Lerp(currentVelocity, moveDirection * targetSpeed, Time.fixedDeltaTime * playerStats.acceleration);
        }

        Vector3 movement = currentVelocity * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);

        RotateTowardsMovementDirection(moveDirection);
    } //NEW

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