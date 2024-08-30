using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Scr_PlayerMove : MonoBehaviour
{
    //Inputs
    [Header("Inputs")]
    public InputActionAsset inputActions;

    //Ground Check
    [Header("Detección de Superficies")]
    public BoxCollider groundDetection;
    public LayerMask groundLayer;

    //Movimiento
    private InputAction moveAction;
    [HideInInspector] public Rigidbody rigidBody;
    [Header("Movimiento")]
    public Vector3 currentMovement;
    public float moveSpeed;

    //Salto
    private InputAction jumpAction;
    [Header("Salto")]
    public float jumpForce;
    public bool jumpBool;

    [Header("Estado")] //New
    public bool canMove;

    private void Awake()
    {
        //Componentes
        rigidBody = GetComponent<Rigidbody>();

        //Inputs
        var actionMap = inputActions.FindActionMap("NormalActions");
        moveAction = actionMap.FindAction("Movement");
        jumpAction = actionMap.FindAction("Jump");
    }

    private void Start()
    {

        canMove = true;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            StickMovement();

            InputActionBool(jumpAction, value => jumpBool = value);
        }
    }

    public bool IsGrounded() //Ground Check
    {
        return Physics.Raycast(groundDetection.bounds.center, Vector3.down,
            groundDetection.bounds.extents.y + 0.1f, groundLayer);
    }

    private void InputActionBool(InputAction inputAction, System.Action<bool> boolAction)
    {
        inputAction.performed += ctx => boolAction(true);
        inputAction.canceled += ctx => boolAction(false);
    }

    public void JumpFunction()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void StickMovement() //Movimiento con Stick
    {
        Vector2 stickInput = moveAction.ReadValue<Vector2>();

        if (IsGrounded())
        {
            currentMovement = new Vector3(stickInput.x, 0, stickInput.y) * moveSpeed * Time.fixedDeltaTime;
            rigidBody.MovePosition(rigidBody.position + currentMovement);

            if (stickInput != Vector2.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(stickInput.x, 0, stickInput.y));
                rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, targetRotation, Time.fixedDeltaTime * 10f));
            }
        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + currentMovement);
        }
    }

}