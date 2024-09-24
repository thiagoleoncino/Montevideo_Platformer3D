using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Prueba : MonoBehaviour
{
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer02StateManager playerState;
    private ScrPlayer05StatsManager playerStats;
    private ScrPlayer06MovementManager playerMove;

    public enum ActionState
    {
        Idle, NormalWalk, FastWalk,
        CrouchIdle, CrouchWalk,
        Attack1, Attack2,
        NeutralJump, MovingJump, Fall, GroundBreak
    }

    public ActionState currentAction;
    private bool playerCanCombo;
    private bool playerIsMoving;
    private bool playerIsCrouching;

    private void Awake()
    {
        playerInputs = GetComponent<ScrPlayer01ControlManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
        playerMove = GetComponent<ScrPlayer06MovementManager>();
        playerStats = GetComponent<ScrPlayer05StatsManager>();
    }

    void FixedUpdate()
    {
        if (playerState.objectCanMove)
        {
            if (playerState.passiveAction || playerState.cancelableAction)
            {
                if (playerState.groundedAction)
                {
                    GroundedMovementActions();
                }
            }
        }
    }

    private void GroundedMovementActions()
    {
        //If the player is Standing or Crouching
        if (playerInputs.InputLeftShoulder1)
        {
            playerIsCrouching = true;
        }
        else
        {
            playerIsCrouching = false;
        }

        //If the player is Moving or not
        if (playerInputs.stickMagnitude == 0)
        {
            playerIsMoving = false;
        }
        else
        {
            playerIsMoving = true;
        }

        //Action Functions
        if (playerIsCrouching)
        {
            HandleCrouchActions();
        }
        else
        {
            HandleStandingActions();
        }
    }

    private void HandleStandingActions()
    {
        playerMove.HandleSticklMovement(playerStats.standingSpeed, true);

        if (playerInputs.stickMagnitude == 0)
        {
            currentAction = ActionState.Idle;
            playerState.passiveAction = true;
            
        }
        else if (playerInputs.stickMagnitude > 0.1 && playerInputs.stickMagnitude < playerInputs.stickThreshold)
        {
            currentAction = ActionState.NormalWalk;
            playerState.cancelableAction = true;
        }
        else if (playerInputs.stickMagnitude > playerInputs.stickThreshold)
        {
            currentAction = ActionState.FastWalk;
            playerState.cancelableAction = true;
        }
    }
    private void HandleCrouchActions()
    {
        playerMove.HandleSticklMovement(playerStats.crouchSpeed, false);

        if (playerInputs.stickMagnitude == 0)
        {
            currentAction = ActionState.CrouchIdle;
            playerState.cancelableAction = true;
        }
        else if (playerInputs.stickMagnitude > 0.1)
        {
            currentAction = ActionState.CrouchWalk;
            playerState.cancelableAction = true;
        }
    }

}
