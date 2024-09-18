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
            if(playerState.groundedAction) //Es en Tierra
            {
                if(playerState.objectCanMove) //Si el jugador puede moverse
                {
                    playerCanMove = true;

                    IdleWalkRunActions();
                }
                else
                {
                    playerCanMove = false;
                    playerIsMoving = false;
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

    public void IdleWalkRunActions()
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
        }
    }
}