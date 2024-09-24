using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ScrPlayer06MovementManager : MonoBehaviour
{
    // Referencias a otros scripts
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer03ActionManager playerActions;
    private ScrPlayer05StatsManager playerStats;

    [HideInInspector] public Rigidbody rigidBody;
    private Transform cameraTransform;
    public Vector3 currentVelocity = Vector3.zero;

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

    public void HandleSticklMovement(float speedStat, bool aceleration_desaceleration)
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

        if (!aceleration_desaceleration) // Si el personaje está agachado, usar crouchSpeed y no aplicara aceleración/desaceleración
        {
            targetSpeed = speedStat;
            currentVelocity = moveDirection * targetSpeed;
        }

        if (aceleration_desaceleration) // Si el personaje está parado, usar walkSpeed y si aplicara aceleración/desaceleración
        {
            float magnitude = stickInput.magnitude;
            targetSpeed = Mathf.Lerp(0, speedStat, magnitude);
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

    public void HandleNeutralJump()
    {
        Vector3 jumpVelocity = Vector3.up * playerStats.jumpForce;
        rigidBody.AddForce(jumpVelocity, ForceMode.VelocityChange);
    }

    public void HandleRunningJump()
    {
        // Conservar el movimiento horizontal
        Vector3 jumpVelocity = Vector3.up * playerStats.jumpForce;

        // Asegúrate de mantener la velocidad actual en el eje X y Z
        jumpVelocity.x = currentVelocity.x;
        jumpVelocity.z = currentVelocity.z;

        // Aplicar la fuerza de salto al Rigidbody
        rigidBody.velocity = jumpVelocity; // Esto garantiza que se mantenga la velocidad en horizontal

        // Puedes añadir aquí más lógica si es necesario, como activar animaciones de salto
    }


}