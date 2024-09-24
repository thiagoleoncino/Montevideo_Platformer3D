using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

public class ScrPlayer07AnimationManager : MonoBehaviour
{
    private ScrPlayer01ControlManager playerInputs;
    private ScrPlayer02StateManager playerState;
    private ScrPlayer03ActionManager playerActions;
    private ScrPlayer06MovementManager playerMove;
    private Animator animator;

    private void Awake()
    {
        playerInputs = GetComponentInParent<ScrPlayer01ControlManager>();
        playerActions = GetComponentInParent<ScrPlayer03ActionManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
        playerMove = GetComponent<ScrPlayer06MovementManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("InputForce", playerInputs.stickInput.magnitude, 0.05f, Time.deltaTime);

        SetAnimation(playerActions.playerIsMoving, "IsMoving");
        SetAnimation(playerActions.playerIsCrouching, "IsCrouching");

        SetAnimation(playerState.groundedAction, "IsGrounded");
        animator.SetFloat("YAxis", playerMove.rigidBody.velocity.y);

    }

    public void SetAnimation(bool action, string animationBool)
    {
        animator.SetBool(animationBool, action);
    }

    public void PlayAnimation(string animation2)
    {
        float transitionDuration = 0.075f;
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (!currentState.IsName(animation2))
        {
            animator.CrossFade(animation2, transitionDuration);
            StartCoroutine(PlayAfterFade(animation2, transitionDuration));
        }
    }

    //NEW
    private IEnumerator PlayAfterFade(string animation, float transitionDuration)
    {
        yield return new WaitForSeconds(transitionDuration);
        animator.Play(animation);
    }

    public void ResetStateEvent()
    {
        animator.SetBool("BackToIdle", true);
        playerActions.currentAction = ScrPlayer03ActionManager.ActionState.Idle;
        playerState.passiveAction = true;
        playerState.objectCanMove = true;
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
    }

    public void AttackComboEvent()
    {
        playerActions.playerCanCombo = true;
    }
}
