using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class ScrPlayer06AnimationManager : MonoBehaviour
{
    private ScrPlayer05MovementManager scriptMove;
    private ScrPlayer01ControlManager scriptInputs;
    private ScrPlayer03ActionManager scriptActions;
    private ScrPlayer02StateManager playerState;
    private Animator animator;

    private void Awake()
    {
        //Componentes
        scriptMove = GetComponentInParent<ScrPlayer05MovementManager>();
        scriptInputs = GetComponentInParent<ScrPlayer01ControlManager>();
        scriptActions = GetComponentInParent<ScrPlayer03ActionManager>();
        playerState = GetComponent<ScrPlayer02StateManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimation(scriptActions.playerIsMoving, "IsMoving");

        animator.SetFloat("InputForce", scriptInputs.stickInput.magnitude, 0.05f, Time.deltaTime); //NEW

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

    public void SetAnimation2(string Action, string AnimationBool)
    {
        if (scriptActions.actualAction == Action)
        {
            animator.SetBool(AnimationBool, true);
        } 
        //else { animator.SetBool(AnimationBool, false); }
    }

    public void SetActionPassive()
    {
        playerState.passiveAction = true;
    }

}
