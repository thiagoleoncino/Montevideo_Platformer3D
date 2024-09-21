using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrBehaviorStopJumping : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Encuentra el GameObject al que est� asociado el animator
        GameObject obj = animator.gameObject;
        ScrPlayer03ActionManager actionManager = obj.GetComponent<ScrPlayer03ActionManager>();

        if (actionManager != null)
        {
            // Apaga la bool playerCanCombo
            actionManager.playerIsJumping = false;
        }
        else
        {
            Debug.LogWarning("No se encontr� el script ScrPlayer03ActionManager en el GameObject.");
        }
    }
}
