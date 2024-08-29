using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerCamera : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction triggerLeftAction;
    private InputAction triggerRightAction;

    public Transform cameraTransform;
    public float cameraMoveSpeed = 2f;

    private void Awake()
    {
        // Obtén las acciones de los gatillos
        var actionMap = inputActions.FindActionMap("ActionOnGround");
        triggerLeftAction = actionMap.FindAction("Trigger Izq");
        triggerRightAction = actionMap.FindAction("Trigger Der");
    }

    private void OnEnable()
    {
        // Activa las acciones de los gatillos
        triggerLeftAction.Enable();
        triggerRightAction.Enable();
    }

    private void OnDisable()
    {
        // Desactiva las acciones de los gatillos
        triggerLeftAction.Disable();
        triggerRightAction.Disable();
    }

    private void Update()
    {
        // Lee la entrada de los gatillos
        float leftTriggerValue = triggerLeftAction.ReadValue<float>();
        float rightTriggerValue = triggerRightAction.ReadValue<float>();

        // Calcula el movimiento de la cámara en función de los gatillos
        float moveAmount = (rightTriggerValue - leftTriggerValue) * cameraMoveSpeed * Time.deltaTime;

        // Mueve la cámara en la dirección del eje Z
        cameraTransform.Translate(0, 0, moveAmount);
    }
}
