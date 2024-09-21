using UnityEngine;

public class ScrBackToIdleBehavior : StateMachineBehaviour
{
    // Este método se ejecuta cuando la transición desde el estado actual ha terminado.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("BackToIdle", false);
    }
}
