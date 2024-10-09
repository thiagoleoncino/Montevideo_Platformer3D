using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ScrPlayer06MovementManager : MonoBehaviour
{
    // Referencias a otros scripts
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer02StateManager playerState; //NEW
    private ScrPlayer03ActionManager playerActions; 
    private ScrPlayer05StatsManager playerStats;

    [HideInInspector] public Rigidbody rigidBody;
    private Transform cameraTransform;
    public Vector3 currentVelocity = Vector3.zero;
    public bool Inertia;
    private Vector3 jumpInertia;         // Para almacenar la velocidad cuando salta

    private void Awake()
    {
        // Inicializar componentes
        rigidBody = GetComponent<Rigidbody>();
        cameraTransform = GameObject.Find("VirtulCamara").transform;

        //Scripts
        playerInputs = GetComponent<ScrPlayer01ControlManager>();
        playerState = GetComponent<ScrPlayer02StateManager>(); //NEW
        playerStats = GetComponent<ScrPlayer05StatsManager>(); 
        playerActions = GetComponent<ScrPlayer03ActionManager>();
    }

    //NEW MODIFICADO
    public void HandleSticklMovement(float speedStat, bool useAcceleration)
    {
        Vector2 stickInput = playerInputs.stickInput;

        // Si no hay input del stick, desacelerar.
        if (stickInput == Vector2.zero)
        {
            ApplyDeceleration();
            return;
        }

        // El personaje siempre se moverá hacia adelante en la dirección que está mirando
        Vector3 moveDirection = transform.forward;

        float targetSpeed;

        // Si no se usa aceleración, asignar la velocidad directa.
        if (!useAcceleration)
        {
            targetSpeed = speedStat;
            currentVelocity = moveDirection * targetSpeed;
        }
        // Si se usa aceleración, ajustar la velocidad de acuerdo al input del stick
        else
        {
            float magnitude = stickInput.magnitude; // Magnitud del input del stick
            targetSpeed = Mathf.Lerp(0, speedStat, magnitude); // Ajusta la velocidad según el input del stick
            currentVelocity = moveDirection * targetSpeed; //NEW
        }

        // Aplicar el movimiento hacia adelante
        Vector3 movement = currentVelocity * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);

        // Mientras se mueve hacia adelante, rotar hacia la dirección del stick
        RotateTowardsStickDirection(stickInput);
    }

    // NEW Rotar el personaje hacia la dirección del stick 
    private void RotateTowardsStickDirection(Vector2 stickInput)
    {
        if (stickInput != Vector2.zero)
        {
            // Calcular la dirección en base al input del stick
            Vector3 targetDirection = CalculateMoveDirection(stickInput);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Suavizar la rotación usando Slerp
            Quaternion newRotation = Quaternion.Slerp(rigidBody.rotation, targetRotation, Time.fixedDeltaTime * playerStats.turnResistance);
            rigidBody.MoveRotation(newRotation);
        }
    }


    private Vector3 CalculateMoveDirection(Vector2 stickInput)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        return (stickInput.x * right + stickInput.y * forward).normalized;
    }



    private void ApplyDeceleration()
    {
        currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, Time.fixedDeltaTime * playerStats.deceleration);
        Vector3 movement = currentVelocity * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);
    }

    public void HandleNeutralJump()
    {
        Vector3 jumpVelocity = Vector3.up * playerStats.jumpForce;
        rigidBody.AddForce(jumpVelocity, ForceMode.VelocityChange);
    }

    public void HandleRunningJump()
    {
        // Almacenar la velocidad en X/Z para mantener la inercia
        jumpInertia = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        // Aplicar fuerza de salto
        rigidBody.AddForce(Vector3.up * playerStats.jumpForce, ForceMode.Impulse);
        Inertia = true;
    }

    public void HandleAirInertia()
    {
        // Multiplicar el movimiento hacia adelante por la magnitud de currentVelocity para adaptarlo a la velocidad actual
        Vector3 forwardMovement = transform.forward * currentVelocity.magnitude;
        rigidBody.velocity = new Vector3(forwardMovement.x, rigidBody.velocity.y, forwardMovement.z); // Mantener la velocidad en Y (gravedad)

        if (Inertia)
        {
            // Si hay inercia, mantener el movimiento hacia adelante
            rigidBody.velocity = new Vector3(forwardMovement.x, rigidBody.velocity.y, forwardMovement.z);

            if(playerState.actualColition) //NEW Detener al impactar contra una superficie
            {
                rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
            }
        }
    }

}