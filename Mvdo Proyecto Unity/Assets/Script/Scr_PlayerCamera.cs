using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerCamera : MonoBehaviour
{
    private Scr_PlayerMove scriptMove;
    public Transform camara;

    public float rotationSpeed = 100f; 

    private void Awake()
    {
        scriptMove = GetComponentInParent<Scr_PlayerMove>();
    }

    private void Update()
    {
        Vector2 stickInput = scriptMove.moveAction.ReadValue<Vector2>();

        float rotationAmount = 0f;

        if (scriptMove.isRunning)
        {
            if (scriptMove.stickDirection == "Izquierda")
            {
                rotationAmount = -rotationSpeed * Time.deltaTime;
            }
            if (scriptMove.stickDirection == "Izquierda Diagonal Arriba" || scriptMove.stickDirection == "Izquierda Diagonal Abajo")
            {
                rotationAmount = (-rotationSpeed * Time.deltaTime) / 2;
            }

            if (scriptMove.stickDirection == "Derecha")
            {
                rotationAmount = rotationSpeed * Time.deltaTime;
            }
            if (scriptMove.stickDirection == "Derecha Diagonal Arriba" || scriptMove.stickDirection == "Derecha Diagonal Abajo")
            {
                rotationAmount = (rotationSpeed * Time.deltaTime) / 2;
            }

            if (rotationAmount != 0f)
            {
                RotateCamera(rotationAmount);
            }
        }
    }

    private void RotateCamera(float rotationAmount)
    {
        // Obtener los ángulos de Euler actuales
        Vector3 currentRotation = camara.eulerAngles;

        // Modificar solo el eje Y
        currentRotation.y += rotationAmount;

        // Aplicar la nueva rotación
        camara.eulerAngles = currentRotation;
    }
}
