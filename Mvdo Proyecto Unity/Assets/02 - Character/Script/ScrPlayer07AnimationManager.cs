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

}
