using UnityEngine;

public class Scr_Prueba : StateMachineBehaviour
{
    // Este m�todo se ejecuta cuando la transici�n desde el estado actual ha terminado.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("BackToIdle", false);
    }
}

