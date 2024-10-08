using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.PointerEventData;

// Direcciones del Stick
public enum EnumStickDirections 
{ 
    Neutral, 
    Up, 
    LeftDiagonalUp, 
    Left, 
    LeftDiagonalDown, 
    Down, 
    RightDiagonalDown, 
    Right, 
    RightDiagonalUp 
}

public class ScrPlayer01ControlManager : MonoBehaviour
{
    //Variables de inputActions
    public InputActionAsset inputActions;
    [HideInInspector] public InputAction inputActionStickIzquierdo;
    [HideInInspector] public InputAction inputActionButton1;
    [HideInInspector] public InputAction inputActionButton2;
    [HideInInspector] public InputAction inputActionButton3;
    [HideInInspector] public InputAction inputActionButton4;
    [HideInInspector] public InputAction inputActionL1;
    [HideInInspector] public InputAction inputActionR1;

    //Variables del Stick Izquierdo
    [Header("Left Stick")]
    public Vector2 stickInput;
    public EnumStickDirections actualStickDirection; //NEW ADD
    public float stickMagnitude;
    public float stickThreshold;

    //NEW Variables del Stikz Izquierdo 2
    [Header("Previous Stick Direction")] 
    public EnumStickDirections previousStickDirection;      // Variable para almacenar la dirección anterior      
    public bool brakeActive = false;                        // Nueva variable para el sistema de freno
    public bool directionChanged;                           // Booleano para detectar un cambio de dirección
    private bool stickDirectionCoroutine = false;           //Se prende cuando se utiliza IEnumerator UpdatePreviousStickDirection
    //NEW

    //Variables de los Botones
    [Header("Buttons")]
    public bool inputButton1; //Equis
    public bool inputButton2; //Cuadrado
    public bool inputButton3; //Circulo
    public bool inputButton4; //Triangulo

    //Variables del Gatillos
    [Header("Triggers")]
    public bool InputLeftShoulder1; //L1
    public bool InputLeftShoulder2; //L2
    public bool InputRightShoulder1; //R1
    public bool InputRightShoulder2; //R2

    //Variables de los Botones extras del control
    [Header("Extras")]
    public bool inputButtonStart; //Start
    public bool inputButtonSelect; //Select


    //Asignacion de Inputs
    private void Awake()
    {
        AssignActions();
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


    //Determinar el Stick y asignar Bools a Inputs
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

    private void DetermineStickInput(Vector2 stickInput)
    {
        // Calcula la magnitud del movimiento del stick
        stickMagnitude = stickInput.magnitude;

        // Si el stick no se está moviendo (si la magnitud muy pequeña), establece la dirección como "Neutral"
        if (stickMagnitude < 0.1f)
        {
            actualStickDirection = EnumStickDirections.Neutral;

            // Iniciar la corrutina solo si no está ya en ejecución
            if (!stickDirectionCoroutine)
            {
                StartCoroutine(PreviousStickDirectionNuetral());
            }

            return; // Salir si el stick no se mueve
        }
        else
        {
            // Si el stick se mueve, cancelar la corrutina si estaba en ejecución
            if (stickDirectionCoroutine)
            {
                StopCoroutine(PreviousStickDirectionNuetral());
                stickDirectionCoroutine = false;
            }
        }

        // Calcular el ángulo del stick en grados
        float angle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        // Determinar la dirección del stick según el ángulo
        if (angle >= 67.5f && angle < 112.5f)
            actualStickDirection = EnumStickDirections.Up;
        else if (angle >= 112.5f && angle < 157.5f)
            actualStickDirection = EnumStickDirections.LeftDiagonalUp;
        else if (angle >= 157.5f && angle < 202.5f)
            actualStickDirection = EnumStickDirections.Left;
        else if (angle >= 202.5f && angle < 247.5f)
            actualStickDirection = EnumStickDirections.LeftDiagonalDown;
        else if (angle >= 247.5f && angle < 292.5f)
            actualStickDirection = EnumStickDirections.Down;
        else if (angle >= 292.5f && angle < 337.5f)
            actualStickDirection = EnumStickDirections.RightDiagonalDown; 
        else if (angle >= 337.5f || angle < 22.5f)
            actualStickDirection = EnumStickDirections.Right;
        else if (angle >= 22.5f && angle < 67.5f)
            actualStickDirection = EnumStickDirections.RightDiagonalUp;

        // Verificar si hay un cambio en la dirección
        if (actualStickDirection != previousStickDirection)
        {
            // Comprobar si el cambio es brusco
            bool IsDirectionReversed(EnumStickDirections currentDirection, EnumStickDirections previousDirection)
            {
                return (previousDirection == EnumStickDirections.Up && currentDirection == EnumStickDirections.Down) ||
                       (previousDirection == EnumStickDirections.Down && currentDirection == EnumStickDirections.Up) ||
                       (previousDirection == EnumStickDirections.Left && currentDirection == EnumStickDirections.Right) ||
                       (previousDirection == EnumStickDirections.Right && currentDirection == EnumStickDirections.Left);
            }

            // SO
            if (IsDirectionReversed(actualStickDirection, previousStickDirection))
            {
                Debug.Log("Dirección cambiada: " + previousStickDirection + " a " + actualStickDirection);
            }

            // Actualizar previousStickDirection despues de verificar el cambio brusco
            previousStickDirection = actualStickDirection;
        }
    }

    private IEnumerator PreviousStickDirectionNuetral()
    {
        stickDirectionCoroutine = true;                         // Marcar que la corrutina está en ejecución
        yield return new WaitForSeconds(0.1f);                  // Esperar 2 segundos
        previousStickDirection = EnumStickDirections.Neutral;   //Cambio a Neutral
        stickDirectionCoroutine = false;                        // Marcar que la corrutina ha finalizado
    }

    private void ConfigureActionBool(InputAction action, System.Action<bool> setBool)
    {
        action.performed += ctx => setBool(true);
        action.canceled += ctx => setBool(false);
    }

}