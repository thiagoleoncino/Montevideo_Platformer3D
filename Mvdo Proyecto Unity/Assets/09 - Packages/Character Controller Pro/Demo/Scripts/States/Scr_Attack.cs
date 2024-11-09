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
        public Animator animator;

        [Space(10)]

        public bool playerIsGrounded;
        public bool punchButton;

        [Space(10)]

        [SerializeField]
        protected string inputAttackParameter = "InputAttack";


        private void Update()
        {
            playerIsGrounded = CharacterActor.IsGrounded;
            punchButton = Input.GetButton("Punch");
        }

        protected override void Awake()
        {
            base.Awake();
        }
        
        // Condiciones para salir de este estado
        public override void CheckExitTransition() 
        {
            // Si la animación actual es Idle
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("001 - Idle"))
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

        public override void PreCharacterSimulation(float dt)
        {
            // Pre/PostCharacterSimulation methods are useful to update all the Animator parameters. 
            // Why? Because the CharacterActor component will end up modifying the velocity of the actor.
            if (!CharacterActor.IsAnimatorValid())
                return;

            CharacterStateController.Animator.SetBool(inputAttackParameter, punchButton);
        }
    }
}