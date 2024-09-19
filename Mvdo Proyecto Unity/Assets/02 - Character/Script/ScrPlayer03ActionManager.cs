using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;
using UnityEngine.Playables;

public class ScrPlayer03ActionManager : MonoBehaviour
{
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer02StateManager playerState;

    public string actualAction;

    [Header("Actions the player can do")]
    public bool playerCanMove = false;

    [Header("Actions the player is doing")]
    public bool playerIsMoving;
    public bool playerIsBreaking;
    public bool playerIsStanding;
    public bool playerIsCrouching;
    public bool playerIsAttacking;

    private void Awake()
    {
        playerInputs = GetComponent<ScrPlayer01ControlManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveStates();

        if(playerState.passiveAction || playerState.cancelableAction) //Si se esta en una accion Pasiva o Cancelable
        {
            if(playerState.groundedAction) //Esta en Tierra
            {
                if (playerState.objectCanMove) //Si el jugador puede moverse
                {
                    HandleGroundedMovementActions();

                    HandleAttackGroundedActions();
                }
            }
        }

    }

    public void HandleMoveStates()
    {
        if (playerState.objectCanMove)
        {
            playerCanMove = true;
        }

        if (playerState.objectSemiMove)
        {
            playerCanMove = false;
        }

        if (playerState.objectCantMove)
        {
            playerCanMove = false;
        }
    }

    public void HandleGroundedMovementActions()
    {
        if(playerInputs.InputLeftShoulder1) //NEW
        {
            playerIsStanding = false;
            playerIsCrouching = true;
        } 
        else { 
            playerIsCrouching = false;
            playerIsStanding = true;
        }

        if(playerIsStanding) 
        {
            if (playerInputs.stickMagnitude == 0)
            {
                actualAction = "Idle";
                playerIsMoving = false;
                playerState.passiveAction = true;
            }

            if (playerInputs.stickMagnitude > 0.1 && playerInputs.stickMagnitude < playerInputs.stickThreshold)
            {
                actualAction = "NormalWalk";
                playerIsMoving = true;
                playerState.cancelableAction = true;
            }

            if (playerInputs.stickMagnitude > playerInputs.stickThreshold)
            {
                actualAction = "FastWalk";
                playerIsMoving = true;
                playerState.cancelableAction = true;

                if (playerInputs.inputButton4) //NEW
                {
                    actualAction = "GroundBreak";

                    playerIsBreaking = true;
                    playerIsMoving = false;

                    playerState.noCancelableAction = true;
                    playerState.objectCantMove = true;
                }
            }
        } //Si el jugador esta Parado

        if (playerIsCrouching) 
        {
            if (playerInputs.stickMagnitude == 0)
            {
                actualAction = "CrouchIdle";
                playerIsMoving = false;
                playerState.passiveAction = true;
            }

            if (playerInputs.stickMagnitude > 0.1)
            {
                actualAction = "CrouchWalk";
                playerIsMoving = true;
                playerState.cancelableAction = true;
            }
        } //Si el jugador esta Agachado
    }

    public void HandleAttackGroundedActions() //NEW
    {
        if (playerInputs.inputButton2)
        {
            actualAction = "Attack1";
            playerIsMoving = false;
            playerIsAttacking = true;
            playerState.objectCantMove = true;
            playerState.noCancelableAction = true;
        }
    }
}