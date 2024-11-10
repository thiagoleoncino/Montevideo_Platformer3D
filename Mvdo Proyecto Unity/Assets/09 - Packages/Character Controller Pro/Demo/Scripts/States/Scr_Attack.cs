using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.CharacterControllerPro.Demo;
using UnityEditor;
using Lightbug.Utilities;

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
        [SerializeField]
        protected string playerGroundedParameter = "PlayerGrounded";

        public CharacterStateController controller;

        // Parámetros de movimiento vertical
        [HideInInspector]
        public VerticalMovementParameters verticalMovementParameters;
        [HideInInspector]
        public MaterialController materialController;

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
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            if (currentState.IsName("001 - Idle"))
            {
                CharacterStateController.EnqueueTransition<NormalMovement>();
            }
        }

        public override bool CheckEnterTransition(CharacterState fromState)
        {
            if (!(controller.CurrentCharacterState is NormalMovement)) //Si el actual estado es NormalMovment
                return false;

            return true;
        }

        public override void UpdateBehaviour(float dt)
        {
            if (playerIsGrounded)
            {
                CharacterActor.PlanarVelocity = Vector3.zero;
            }
            else
            {
                // Aplica la gravedad cuando el personaje no está en el suelo
                ProcessGravity(dt);
            }
        }

        public override void PreCharacterSimulation(float dt)
        {
            if (!CharacterActor.IsAnimatorValid())
                return;

            CharacterStateController.Animator.SetBool(inputAttackParameter, punchButton);
            CharacterStateController.Animator.SetBool(playerGroundedParameter, playerIsGrounded);
        }

        // Función para procesar la gravedad cuando el personaje no está en el suelo
        protected virtual void ProcessGravity(float dt)
        {
            if (!verticalMovementParameters.useGravity)
                return;

            verticalMovementParameters.UpdateParameters();

            float gravityMultiplier = 1f;

            if (materialController != null)
            {
                gravityMultiplier = CharacterActor.LocalVelocity.y >= 0 ?
                    materialController.CurrentVolume.gravityAscendingMultiplier :
                    materialController.CurrentVolume.gravityDescendingMultiplier;
            }

            float gravity = gravityMultiplier * verticalMovementParameters.gravity;

            if (!CharacterActor.IsStable)
            {
                CharacterActor.VerticalVelocity += CustomUtilities.Multiply(-CharacterActor.Up, gravity, dt);
            }
        }
    }
}