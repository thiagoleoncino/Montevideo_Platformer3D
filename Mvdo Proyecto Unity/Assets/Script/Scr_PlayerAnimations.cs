using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerAnimations : MonoBehaviour
{
    private Scr_PlayerMove scriptMove;
    private Scr_PlayerInputs scriptInputs;
    private Scr_PlayerActions scriptActions;
    private Animator animator;

    private void Awake()
    {
        //Componentes
        scriptMove = GetComponentInParent<Scr_PlayerMove>();
        scriptInputs = GetComponentInParent<Scr_PlayerInputs>();
        scriptActions = GetComponentInParent<Scr_PlayerActions>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Si se puede mover
        SetAnimation(scriptMove.canMove, "CanMove");

        //Animacion de Caminar
        SetAnimation(scriptInputs.stickInput != Vector2.zero, "IsMoving");

        //Animacion de Correr
        SetAnimation(scriptMove.isRunning, "IsRunning");

        SetAnimation(scriptInputs.InputLeftShoulder, "IsCrouching"); //NEW

        SetAnimation(scriptActions.IsAttacking, "IsAttacking"); //NEW

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
