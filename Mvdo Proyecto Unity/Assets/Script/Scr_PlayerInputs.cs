using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerInputs : MonoBehaviour
{
    [Header("Stick Izquierdo")]
    public InputActionAsset inputActions;
    public Vector2 stickInput;
    public string stickDirection;
    public float stickThreshold = 0.5f;

    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public InputAction crouchAction; //NEW
    [HideInInspector] public InputAction attackAction; //NEW
    private Scr_PlayerMove playerMove;

    [Header("Botones")]
    public bool inputButton1; //X
    public bool inputButton2; //Cuadrado
    public bool inputButton3; //O
    public bool inputButton4; //Triangulo
    public bool InputLeftShoulder;
    public bool InputRightShoulder;


    private void Awake()
    {
        // Inicializar el Action Map y la acción de movimiento
        var actionMap = inputActions.FindActionMap("NormalActions");
        moveAction = actionMap.FindAction("Movement");
        crouchAction = actionMap.FindAction("Crouch"); //NEW
        attackAction = actionMap.FindAction("Attack"); //NEW

        // Referencia al script de movimiento
        playerMove = GetComponent<Scr_PlayerMove>();
    }

    private void OnEnable()
    {
        // Habilitar las acciones
        moveAction.Enable();

        // Configurar la acción de agacharse para activar o desactivar el bool NEW
        ConfigureActionBool(crouchAction, value => InputLeftShoulder = value);

        ConfigureActionBool(attackAction, value => inputButton2 = value);
    }

    private void OnDisable()
    {
        // Deshabilitar las acciones
        moveAction.Disable();
        crouchAction.Disable();
        attackAction.Disable();
    }

    private void Update()
    {
        stickInput = moveAction.ReadValue<Vector2>();
        DetermineStickState(stickInput);

        // Pasar los inputs al script de movimiento
        playerMove.UpdateMovementInput(stickInput, stickDirection);

    }

    private void DetermineStickState(Vector2 stickInput)
    {
        float angle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        float magnitude = stickInput.magnitude;

        // Determinar dirección del stick
        stickDirection = GetStickDirection(angle);

        // Determinar zona del stick
        string zone = magnitude < stickThreshold ? "Círculo Interno" : "Círculo Externo";
        playerMove.isRunning = magnitude >= stickThreshold;
    }

    private string GetStickDirection(float angle)
    {
        if (angle >= 67.5f && angle < 112.5f)
            return "Arriba";
        else if (angle >= 112.5f && angle < 157.5f)
            return "Izquierda Diagonal Arriba";
        else if (angle >= 157.5f && angle < 202.5f)
            return "Izquierda";
        else if (angle >= 202.5f && angle < 247.5f)
            return "Izquierda Diagonal Abajo";
        else if (angle >= 247.5f && angle < 292.5f)
            return "Abajo";
        else if (angle >= 292.5f && angle < 337.5f)
            return "Derecha Diagonal Abajo";
        else if (angle >= 337.5f || angle < 22.5f)
            return "Derecha";
        else if (angle >= 22.5f && angle < 67.5f)
            return "Derecha Diagonal Arriba";

        return "Neutral";
    }

    private void ConfigureActionBool(InputAction action, System.Action<bool> setBool)
    {
        action.Enable();
        action.performed += ctx => setBool(true);
        action.canceled += ctx => setBool(false);
    }
}

