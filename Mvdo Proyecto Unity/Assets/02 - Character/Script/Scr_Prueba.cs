using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Implementation;

public class Scr_Prueba : MonoBehaviour
{
    public bool playerIsPunching;
    public Animator Anim;

    void Update()
    {
        BoolActivation();
    }

    public void BoolActivation()
    {
        if (!playerIsPunching)
        {
            if (Input.GetButton("Punch"))
            {
                playerIsPunching = true; // Activar la acción
                Anim.SetBool("NewAction", true);
                Anim.SetFloat("NewGroundedAction", 1f);
            }
        }
    }
}
