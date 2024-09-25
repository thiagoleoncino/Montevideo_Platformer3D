using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrBehaviorStopJumping : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1.0f)
        {
            GameObject obj = animator.gameObject;
            ScrPlayer02StateManager stateManager = obj.GetComponent<ScrPlayer02StateManager>();
            Scr_Prueba prueba = obj.GetComponent<Scr_Prueba>();
            stateManager.passiveAction = true;
            prueba.Inertia = false;
        }
    }
}
