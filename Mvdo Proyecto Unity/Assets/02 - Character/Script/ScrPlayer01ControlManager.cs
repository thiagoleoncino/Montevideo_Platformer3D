using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.PointerEventData;

public class ScrPlayer01ControlManager : MonoBehaviour
{
    public InputActionAsset inputActions;

    [HideInInspector] public InputAction inputActionStickIzquierdo;
    [HideInInspector] public InputAction inputActionButton1;
    [HideInInspector] public InputAction inputActionButton2;
    [HideInInspector] public InputAction inputActionButton3;
    [HideInInspector] public InputAction inputActionButton4;
    [HideInInspector] public InputAction inputActionL1;
    [HideInInspector] public InputAction inputActionR1;

    [Header("Stick Izquierdo")]
    public Vector2 stickInput;
    public string stickDirection;
    public float stickMagnitude;
    public float stickThreshold;

    [Header("Botones")]
    public bool inputButton1; //Equis
    public bool inputButton2; //Cuadrado
    public bool inputButton3; //Circulo
    public bool inputButton4; //Triangulo

    [Header("Gatillos")]
    public bool InputLeftShoulder1; //L1
    public bool InputLeftShoulder2; //L2

    public bool InputRightShoulder1; //R1
    public bool InputRightShoulder2; //R2

    [Header("Extras")]
    public bool inputButtonStart; //Start
    public bool inputButtonSelect; //Select

    private void Awake()
    {
        AssignActions();
    }

    protected virtual void OnEnable() => inputActions?.Enable();
    protected virtual void OnDisable() => inputActions?.Disable();

    private void Update()
    {
        stickInput = inputActionStickIzquierdo.ReadValue<Vector2>();
        DetermineStickInput(stickInput);

        ConfigureActionBool(inputActionButton1, value => inputButton1 = value);
        ConfigureActionBool(inputActionButton2, value => inputButton2 = value);
        ConfigureActionBool(inputActionButton3, value => inputButton3 = value);
        ConfigureActionBool(inputActionButton4, value => inputButton4 = value);

        ConfigureActionBool(inputActionL1, value => InputLeftShoulder1 = value);
        ConfigureActionBool(inputActionR1, value => InputRightShoulder1 = value);
    }

    private void AssignActions()
    {
        inputActionStickIzquierdo = inputActions["LeftStick"];
        inputActionButton1 = inputActions["Button 1"];
        inputActionButton2 = inputActions["Button 2"];
        inputActionButton3 = inputActions["Button 3"];
        inputActionButton4 = inputActions["Button 4"];
        inputActionL1 = inputActions["L1"];
        inputActionR1 = inputActions["R1"];
    }

    private void DetermineStickInput(Vector2 stickInput)
    {
        // Calcular la magnitud del movimiento del stick
        stickMagnitude = stickInput.magnitude;

        // Si el stick no se est� moviendo (magnitud muy peque�a), establecer la direcci�n como "Neutral"
        if (stickMagnitude < 0.1f) // Puedes ajustar este valor si lo prefieres
        {
            stickDirection = "Neutral";
            return;
        }

        // Calcular el �ngulo del stick en grados
        float angle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        // Determinar la direcci�n del stick seg�n el �ngulo
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
        else
            stickDirection = "Neutral";
    }

    private void ConfigureActionBool(InputAction action, System.Action<bool> setBool)
    {
        action.performed += ctx => setBool(true);
        action.canceled += ctx => setBool(false);
    }

}