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

        public override void UpdateBehaviour(float dt)
        {

        }
    }
}


