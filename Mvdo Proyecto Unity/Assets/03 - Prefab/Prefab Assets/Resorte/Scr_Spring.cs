using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Spring : MonoBehaviour
{
    // Magnitud de la fuerza de rebote
    public float bounceForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        SrpingEffect(other);
    }

    private void OnTriggerStay(Collider other)
    {
       // SrpingEffect(other);
    }

    public void SrpingEffect(Collider other2)
    {
        // Verifica si el objeto con el que colisiona es el jugador
        if (other2.CompareTag("Player"))
        {
            // Obtiene el Rigidbody del jugador
            Rigidbody playerRb = other2.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Ajusta la posición del jugador a la del resorte antes de lanzarlo
                other2.transform.position = transform.position;

                // Resetea la velocidad del jugador a cero
                playerRb.velocity = Vector3.zero;

                // Aplica una fuerza en la dirección hacia la que está mirando el objeto
                Vector3 bounceDirection = transform.forward;
                playerRb.velocity = bounceDirection * bounceForce;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibuja una línea en la dirección de forward con longitud basada en bounceForce
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * bounceForce / 2);
    }
}
