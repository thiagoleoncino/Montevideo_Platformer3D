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
        //Si se puede mover
        SetAnimation(scriptMove.canMove, "CanMove"); 

        //Animacion de Caminar
        SetAnimation(scriptMove.stickInput != Vector2.zero, "IsMoving");

        //Animacion de Correr
        SetAnimation(scriptMove.isRunning, "IsRunning");
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

}
