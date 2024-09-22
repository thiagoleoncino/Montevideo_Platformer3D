using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.PointerEventData;

public class ScrPlayer01ControlManager : MonoBehaviour
{
    private ScrPlayer03ActionManager playerActions;

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

    // Nueva variable para el sistema de freno
    public bool brakeActive = false;
    public string previousStickDirection; // Variable para almacenar la dirección anterior
    public bool directionChanged; // Booleano para detectar un cambio de dirección

    private void Awake()
    {
        AssignActions();
        playerActions = GetComponentInParent<ScrPlayer03ActionManager>();
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

    // Variable para controlar si la corrutina está en ejecución
    private bool isNeutralCoroutineRunning = false;

    private void DetermineStickInput(Vector2 stickInput)
    {
        // Calcular la magnitud del movimiento del stick
        stickMagnitude = stickInput.magnitude;

        // Si el stick no se está moviendo (magnitud muy pequeña), establecer la dirección como "Neutral"
        if (stickMagnitude < 0.1f)
        {
            stickDirection = "Neutral";

            // Iniciar la corrutina solo si no está ya en ejecución
            if (!isNeutralCoroutineRunning)
            {
                StartCoroutine(UpdatePreviousStickDirectionWithDelay());
            }

            return; // Salir si el stick no se mueve
        }
        else
        {
            // Si el stick se mueve, cancelar la corrutina si estaba en ejecución
            if (isNeutralCoroutineRunning)
            {
                StopCoroutine(UpdatePreviousStickDirectionWithDelay());
                isNeutralCoroutineRunning = false;
            }
        }

        // Calcular el ángulo del stick en grados
        float angle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        // Determinar la dirección del stick según el ángulo
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

        // Verificar si hay un cambio abrupto en la dirección y si la magnitud es mayor que el umbral
        if (stickDirection != previousStickDirection)
        {
            // Comprobar si el cambio es brusco
            if ((previousStickDirection == "Arriba" && stickDirection == "Abajo") ||
                (previousStickDirection == "Abajo" && stickDirection == "Arriba") ||
                (previousStickDirection == "Izquierda" && stickDirection == "Derecha") ||
                (previousStickDirection == "Derecha" && stickDirection == "Izquierda"))
            {
                //directionChanged = true; // Activar la booleano
                Debug.Log("Dirección cambiada: " + previousStickDirection + " a " + stickDirection);
            }

            // Actualizar previousStickDirection solo si hubo un cambio efectivo
            previousStickDirection = stickDirection;
        }

        if (directionChanged)
        {
            previousStickDirection = null;
        }
    }

    // Corrutina para retrasar la actualización de previousStickDirection a "Neutral"
    private IEnumerator UpdatePreviousStickDirectionWithDelay()
    {
        isNeutralCoroutineRunning = true; // Marcar que la corrutina está en ejecución
        yield return new WaitForSeconds(0.1f); // Esperar 2 segundos (ajusta este valor según sea necesario)
        previousStickDirection = "Neutral";
        isNeutralCoroutineRunning = false; // Marcar que la corrutina ha finalizado
    }

    private void ConfigureActionBool(InputAction action, System.Action<bool> setBool)
    {
        action.performed += ctx => setBool(true);
        action.canceled += ctx => setBool(false);
    }
}
