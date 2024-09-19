using UnityEngine;

public class ScrPlayer08BackToIdle : StateMachineBehaviour
{
    // Este método se ejecuta cuando la transición desde el estado actual ha terminado.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("BackToIdle", false);
    }
}
