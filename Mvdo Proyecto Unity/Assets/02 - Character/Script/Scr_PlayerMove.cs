using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerMove : MonoBehaviour
{
    [Header("Estado")]
    public bool canMove = true;
    public bool isRunning = false;

    [Header("Movimiento")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    [HideInInspector] public Rigidbody rigidBody;
    private Transform cameraTransform;

    private Vector2 currentStickInput;
    private string currentStickDirection;
    private Scr_PlayerInputs playerInputs;  // Asegúrate de asignar esta referencia en el Inspector
    private Scr_PlayerAnimations playerAnimations;
    private Scr_PlayerActions playerActions; //NEW

    private void Awake()
    {
        // Inicializar componentes
        rigidBody = GetComponent<Rigidbody>();
        cameraTransform = GameObject.Find("VirtulCamara").transform;
        playerInputs = GetComponent<Scr_PlayerInputs>(); //NEW
        playerActions = GetComponent<Scr_PlayerActions>(); //NEW
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

        BlockMotion(playerActions.IsAttacking);
    }

    public void UpdateMovementInput(Vector2 stickInput, string stickDirection)
    {
        currentStickInput = stickInput;
        currentStickDirection = stickDirection;
    }

    private void HandleMovement()
    {
        if (currentStickInput == Vector2.zero)
        {
            isRunning = false;
            return;
        }

        Vector3 moveDirection = CalculateMoveDirection(currentStickInput);

        // Determina la velocidad en función del estado de boolCrouch NEW
        float speed = playerInputs.InputLeftShoulder ? crouchSpeed : (isRunning ? runSpeed : walkSpeed); 

        Vector3 movement = moveDirection * speed * Time.fixedDeltaTime;

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
            rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }
    }

    private void BlockMotion(bool InputBool)
    {
        if(InputBool)
        {
            canMove = false;
        }

        if (!InputBool)
        {
            canMove = true;
        }
    }
}
