using UnityEngine;

public class ScrBehaviorStopCombo : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Encuentra el GameObject al que está asociado el animator
        GameObject obj = animator.gameObject;
        ScrPlayer03ActionManager actionManager = obj.GetComponent<ScrPlayer03ActionManager>();

        if (actionManager != null)
        {
            // Apaga la bool playerCanCombo
            actionManager.playerCanCombo = false;
        }
        else
        {
            Debug.LogWarning("No se encontró el script ScrPlayer03ActionManager en el GameObject.");
        }
    }
}
