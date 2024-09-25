using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayer03ActionManager : MonoBehaviour
{
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer02StateManager playerState;
    private ScrPlayer05StatsManager playerStats;
    private ScrPlayer06MovementManager playerMove;
    private ScrPlayer07AnimationManager playerAnimator;
    public Scr_Prueba prueba;

    public enum ActionState
    {
        Idle, NormalWalk, FastWalk,
        CrouchIdle, CrouchWalk,
        Attack1, Attack2,
        NeutralJump, RuningJump, Fall, GroundBreak
    }

    public ActionState currentAction;

    public bool playerIsMoving;
    public bool playerIsCrouching;

    public bool playerCanCombo;

    private void Awake()
    {
        playerInputs = GetComponent<ScrPlayer01ControlManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
        playerMove = GetComponent<ScrPlayer06MovementManager>();
        playerStats = GetComponent<ScrPlayer05StatsManager>();
        playerAnimator = GetComponent<ScrPlayer07AnimationManager>();
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
                    HandleGroundedAttackActions();
                    HandleJumpActions();
                }

            }

            if (playerState.airbornAction)
            {
                prueba.HandleAirMovement();
            }

            if (playerState.passiveAction)
            {
                if (playerState.airbornAction)
                {
                    HandleAirbornActions();
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
    private void HandleGroundedAttackActions()
    {
        if (playerInputs.inputButton2 && !playerCanCombo)
        {
            currentAction = ActionState.Attack1;
            playerState.noCancelableAction = true;
            playerAnimator.PlayAnimation("007 - Attack1");
        }

        if (playerInputs.inputButton2 && playerCanCombo)
        {
            currentAction = ActionState.Attack2;
            playerState.noCancelableAction = true;
            playerAnimator.PlayAnimation("008 - Attack2");
        }
    }

    private void HandleAirbornActions()
    {
        //playerMove.HandleSticklMovement(playerStats.airMoveSpeed, false);
        currentAction = ActionState.Fall;
    }
    public void HandleJumpActions()
    {
        if(playerInputs.inputButton1)
        {
            if (!playerIsMoving)
            {
                playerMove.HandleNeutralJump();
                currentAction = ActionState.NeutralJump;
                playerState.cancelableAction = true;
                playerAnimator.PlayAnimation("009 - Jump");
            }

            if (playerIsMoving)
            {
                prueba.HandleJump();
                currentAction = ActionState.RuningJump;
                playerState.cancelableAction = true;
                playerAnimator.PlayAnimation("010 - RunJump");
            }
        }

    }

}
