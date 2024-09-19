using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

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

        PlayAnimation("GroundBreak", "04 - RunStop"); //NEW

        SetAnimation(playerActions.playerIsMoving, "IsMoving"); //NEW

        SetAnimation(playerActions.playerIsCrouching, "IsCrouching"); //NEW

        PlayAnimation("Attack1", "07 - Attack1"); //NEW

        PlayAnimation("Attack2", "08 - Attack2"); //NEW

       SetAnimation(playerActions.playerCanCombo, "CanCombo"); //NEW

       SetAnimation(playerInputs.inputButton2, "AttackButton"); //NEW
    }

    public void SetAnimation(bool Action, string AnimationBool)
    {
        if (Action)
        {
            animator.SetBool(AnimationBool, true);
        }
        if (!Action)
        {
            animator.SetBool(AnimationBool, false);
        }
    }

    public void PlayAnimation(string ActualAction, string Animation)
    {
        if (playerActions.actualAction == ActualAction)
        {
            animator.Play(Animation);
        }
    }

    public void ResetStateEvent()
    {
        animator.SetBool("BackToIdle", true); //NEW
        playerState.passiveAction = true;
        playerState.objectCanMove = true;
    }

    public void FalseActionByName(string actionBoolName)
    {
        var field = typeof(ScrPlayer03ActionManager).GetField(actionBoolName);

        if (field != null && field.FieldType == typeof(bool))
        {
            field.SetValue(playerActions, false);
            //Debug.Log($"{eventName} en scriptActions ha sido establecido a false");
        }
        else
        {
            Debug.LogWarning($"No se encontró ninguna propiedad bool con el nombre: {actionBoolName} en scriptActions");
        }

        playerActions.playerCanCombo = false;

    } //NEW

    public void AttackComboEvent()
    {
        playerActions.playerCanCombo = true;
    }

}