using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPlayer03ActionManager : MonoBehaviour
{
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer02StateManager playerState;
    private ScrPlayer06MovementManager playerMove;

    public enum ActionState 
    { 
        Idle, NormalWalk, FastWalk, 
        CrouchIdle, CrouchWalk, 
        Attack1, Attack2, 
        NeutralJump, GroundBreak 
    }

    public ActionState currentAction;

    [Header("Actions the player can do")]
    public bool playerCanMove;
    public bool playerCanCombo;

    [Header("Actions the player is doing")]
    public bool playerIsMoving;
    public bool playerIsCrouching;
    public bool playerIsAttacking;
    public bool playerIsJumping;

    private void Awake()
    {
        playerInputs = GetComponent<ScrPlayer01ControlManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
        playerMove = GetComponent<ScrPlayer06MovementManager>();
    }

    void FixedUpdate()
    {
        HandleMoveStates();

        if (playerState.objectCanMove && (playerState.passiveAction || playerState.cancelableAction))
        {
            if (playerState.groundedAction)
            {
                GroundedMovementActions();
                HandleGroundedAttackActions();
            }
        }

        HandleJumpActions();
    }

    private void HandleMoveStates()
    {
        playerCanMove = playerState.objectCanMove && !playerState.objectSemiMove && !playerState.objectCantMove;
    }

    private void GroundedMovementActions()
    {
        playerMove.HandleHorizontalMovement();

        if (playerInputs.InputLeftShoulder1)
        {
            playerIsCrouching = true;
        }
        else
        {
            playerIsCrouching = false;
        }

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
        if (playerInputs.stickMagnitude == 0)
        {
            currentAction = ActionState.Idle;
            playerIsMoving = false;
            playerState.passiveAction = true;
        }
        else if (playerInputs.stickMagnitude > 0.1 && playerInputs.stickMagnitude < playerInputs.stickThreshold)
        {
            currentAction = ActionState.NormalWalk;
            playerIsMoving = true;
            playerState.cancelableAction = true;
        }
        else if (playerInputs.stickMagnitude > playerInputs.stickThreshold)
        {
            currentAction = ActionState.FastWalk;
            playerIsMoving = true;
            playerState.cancelableAction = true;

            if (playerInputs.directionChanged)
            {
                currentAction = ActionState.GroundBreak;
                HandleGroundBreak();
            }
        }
    }

    private void HandleCrouchActions()
    {
        if (playerInputs.stickMagnitude == 0)
        {
            currentAction = ActionState.CrouchIdle;
            playerIsMoving = false;
            playerState.passiveAction = true;
        }
        else if (playerInputs.stickMagnitude > 0.1)
        {
            currentAction = ActionState.CrouchWalk;
            playerIsMoving = true;
            playerState.cancelableAction = true;
        }
    }
    private void HandleGroundBreak()
    {
        playerIsMoving = false;
        playerState.noCancelableAction = true;
        playerState.objectCantMove = true;
    }

    private void HandleGroundedAttackActions()
    {
        if (playerInputs.inputButton2 && !playerCanCombo)
        {
            currentAction = ActionState.Attack1;
            playerIsMoving = false;
            playerIsAttacking = true;
            playerState.objectCantMove = true;
            playerState.cancelableAction = true;
        }

        if (playerInputs.inputButton2 && playerCanCombo)
        {
            currentAction = ActionState.Attack2;
            playerIsMoving = false;
            playerIsAttacking = true;
            playerState.objectCantMove = true;
            playerState.noCancelableAction = true;
        }
    }

    public void HandleJumpActions()
    {
        if (playerState.groundedAction && playerInputs.inputButton1)
        {
            playerMove.HandleVerticalMovement();
            currentAction = ActionState.NeutralJump;
            playerIsMoving = true;
            playerIsJumping = true;
            playerState.cancelableAction = true;
            playerState.objectCantMove= true;
        }
    }

    public void AirbornMovementActions()
    {

    }
}
