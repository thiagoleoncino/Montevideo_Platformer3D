using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

public class ScrPlayer07AnimationManager : MonoBehaviour
{
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer02StateManager playerState;
    private ScrPlayer03ActionManager playerActions;
    private Animator animator;

    private void Awake()
    {
        playerInputs = GetComponentInParent<ScrPlayer01ControlManager>();
        playerActions = GetComponentInParent<ScrPlayer03ActionManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("InputForce", playerInputs.stickInput.magnitude, 0.05f, Time.deltaTime);

        // Maneja la animación de GroundBreak
        PlayAnimation(ScrPlayer03ActionManager.ActionState.GroundBreak, "04 - RunStop");

        // Maneja las animaciones de movimiento y estado
        SetAnimation(playerActions.playerIsMoving, "IsMoving");
        SetAnimation(playerActions.playerIsCrouching, "IsCrouching");
        SetAnimation(playerActions.playerIsJumping, "IsJumping");

        // Maneja las animaciones de ataque
        PlayAnimation(ScrPlayer03ActionManager.ActionState.Attack1, "07 - Attack1");
        PlayAnimation(ScrPlayer03ActionManager.ActionState.Attack2, "08 - Attack2");
    }

    public void SetAnimation(bool action, string animationBool)
    {
        animator.SetBool(animationBool, action);
    }

    public void PlayAnimation(ScrPlayer03ActionManager.ActionState actualAction, string animation)
    {
        if (playerActions.currentAction == actualAction)
        {
            animator.Play(animation);
        }
    }

    public void ResetStateEvent()
    {
        animator.SetBool("BackToIdle", true);
        playerActions.currentAction = ScrPlayer03ActionManager.ActionState.Idle;
        playerState.passiveAction = true;
        playerState.objectCanMove = true;
        playerInputs.directionChanged = false;
    }

    public void FalseActionByName(string actionBoolName)
    {
        var field = typeof(ScrPlayer03ActionManager).GetField(actionBoolName);

        if (field != null && field.FieldType == typeof(bool))
        {
            field.SetValue(playerActions, false);
        }
        else
        {
            Debug.LogWarning($"No se encontró ninguna propiedad bool con el nombre: {actionBoolName} en scriptActions");
        }

        playerActions.playerCanCombo = false;
    }

    public void AttackComboEvent()
    {
        playerActions.playerCanCombo = true;
    }
}
