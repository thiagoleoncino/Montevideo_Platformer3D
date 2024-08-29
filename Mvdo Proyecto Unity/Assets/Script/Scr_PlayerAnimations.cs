using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerAnimations : MonoBehaviour
{
    private Scr_PlayerMove scriptMove;
    private Animator animator;

    private void Awake()
    {
        //Componentes
        scriptMove = GetComponentInParent<Scr_PlayerMove>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimation(scriptMove.IsGrounded(), "IsGrounded");

        SetAnimation(scriptMove.currentMovement != Vector3.zero, "IsMoving");

        SetAnimation(scriptMove.jumpBool, "IsJumping");
    }

    public void SetAnimation(bool Action, string Animation)
    {
        if (Action)
        {
            animator.SetBool(Animation, true);
        }
        if (!Action)
        {
            animator.SetBool(Animation, false);
        }
    }

    public void JumpEvent()
    {
        scriptMove.JumpFunction();
    }

}
