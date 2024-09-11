using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerActions : MonoBehaviour
{
    private Scr_PlayerInputs playerInputs; 
    private Animator animator;

    public string actualAction;

    public bool IsAttacking;


    private void Awake()
    {
        playerInputs = GetComponent<Scr_PlayerInputs>(); //NEW
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OneTimeAction(playerInputs.inputButton2, ref IsAttacking, "06 - NormalAttack1");
    }

    public void OneTimeAction(bool Input, ref bool Action, string AnimationName)
    {
        if (Input)
        {
            Action = true;
        }
        else
        {
            // Obtener la información del estado actual de la animación
            AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

            // Verificar si la animación ha terminado
            if (currentAnimation.IsName(AnimationName) && currentAnimation.normalizedTime >= 1.0f)
            {
                Action = false;
            }
        }
    }

}
