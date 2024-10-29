using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.CharacterControllerPro.Demo;
using UnityEditor;

namespace Lightbug.CharacterControllerPro.Demo
{
    [AddComponentMenu("Character Controller Pro/Demo/Character/States/Attack")]

    public class Scr_Attack : CharacterState
    {
        public bool punchButton;
        public Animator animator;
        public NormalMovement moveScript;

        private void Update()
        {
            punchButton = Input.GetButton("Punch");
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void CheckExitTransition() // Condiciones para salir de este estado
        {
            // Si la animación actual ha terminado, transiciona a NormalMovement
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("01 - Attack") && currentState.normalizedTime >= 1f)
            {
                CharacterStateController.EnqueueTransition<NormalMovement>(); // Se pone el estado en cola
            }
        }

        // Write your transitions here
        public override bool CheckEnterTransition(CharacterState fromState)
        {
            if (!CharacterActor.IsGrounded)
                return false;

            return true;
        }

        // Write your update code here
        public override void UpdateBehaviour(float dt)
        {
            CharacterActor.PlanarVelocity = Vector3.zero;
        }
    }
}