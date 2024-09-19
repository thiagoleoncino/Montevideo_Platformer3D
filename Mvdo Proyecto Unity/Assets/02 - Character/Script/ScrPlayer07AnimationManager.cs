using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class ScrPlayer07AnimationManager : MonoBehaviour
{
    private ScrPlayer01ControlManager scriptInputs;
    private ScrPlayer02StateManager playerState;
    private ScrPlayer03ActionManager scriptActions;
    private Animator animator;

    private void Awake()
    {
        scriptInputs = GetComponentInParent<ScrPlayer01ControlManager>();
        scriptActions = GetComponentInParent<ScrPlayer03ActionManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimation(scriptActions.playerIsMoving, "IsMoving");

        animator.SetFloat("InputForce", scriptInputs.stickInput.magnitude, 0.05f, Time.deltaTime);

        SetAnimation(scriptActions.playerIsBreaking, "IsBreaking"); //NEW

        SetAnimation(scriptActions.playerIsCrouching, "IsCrouching"); //NEW

        SetAnimation(scriptActions.playerIsAttacking, "IsAttacking"); //NEW
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

    public void ResetStateEvent()
    {
        playerState.passiveAction = true;
        playerState.objectCanMove = true;
    }

    public void FalseActionByName(string actionBoolName)
    {
        var field = typeof(ScrPlayer03ActionManager).GetField(actionBoolName);

        if (field != null && field.FieldType == typeof(bool))
        {
            field.SetValue(scriptActions, false);
            //Debug.Log($"{eventName} en scriptActions ha sido establecido a false");
        }
        else
        {
            Debug.LogWarning($"No se encontró ninguna propiedad bool con el nombre: {actionBoolName} en scriptActions");
        }
    } //NEW


}
