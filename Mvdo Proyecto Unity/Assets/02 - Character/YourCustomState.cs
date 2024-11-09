using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.CharacterControllerPro.Demo;
using UnityEditor;

namespace Lightbug.CharacterControllerPro.Demo
{
    [AddComponentMenu("Character Controller Pro/Demo/Character/States/YourCustomState")]

    public class YourCustomState : CharacterState
    {
        //Update se actualiza en todo momento
        private void Update()
        {

        }

        protected override void Awake()
        {
            base.Awake();
        }

        // Condiciones para salir de este estado
        public override void CheckExitTransition()
        {

        }

        // Condiciones para entrar en este estado
        public override bool CheckEnterTransition(CharacterState fromState)
        {
            return true;
        }

        //UpdateBehaviour se actualiza cuando el estado esta en uso
        public override void UpdateBehaviour(float dt)
        {

        }

        public override void PreCharacterSimulation(float dt)
        {
            // Pre/PostCharacterSimulation methods are useful to update all the Animator parameters. 
            // Why? Because the CharacterActor component will end up modifying the velocity of the actor.
            if (!CharacterActor.IsAnimatorValid())
                return;

        }
    }
}


