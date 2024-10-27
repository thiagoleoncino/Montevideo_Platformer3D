using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.CharacterControllerPro.Demo;

namespace Lightbug.CharacterControllerPro.Demo
{
    [AddComponentMenu("Character Controller Pro/Demo/Character/States/Attack")]

    public class Scr_Attack : CharacterState
    {
        public bool punchButton;
        private Animator animator;

        private void Update()
        {
            punchButton = Input.GetButton("Punch");
        }

        protected override void Awake()
        {
            base.Awake();
            animator = CharacterActor.Animator;
        }

        public override void CheckExitTransition() //Condiciones para salir de este estado
        {
            if (punchButton) //Si el boton es presionado
            {
                //CharacterStateController.EnqueueTransition<NormalMovement>(); //Se pone el estado en cola
            }
        }

        // Write your transitions here
        public override bool CheckEnterTransition(CharacterState fromState)
        {
            return base.CheckEnterTransition(fromState);
        }

        // Write your update code here
        public override void UpdateBehaviour(float dt)
        {
        }
    }
}

