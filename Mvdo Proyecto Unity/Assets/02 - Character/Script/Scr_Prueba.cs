using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Prueba : MonoBehaviour
{
    public float moveSpeed = 5f;         // Velocidad de movimiento en X/Z
    public float jumpForce = 7f;         // Fuerza del salto
    public bool Inertia = false;       // Para verificar si está en el suelo
    private Rigidbody rb;
    private Vector3 jumpInertia;         // Para almacenar la velocidad cuando salta
    private ScrPlayer01ControlManager control;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        control = GetComponent<ScrPlayer01ControlManager>();
    }

    void Update()
    {
        //HandleGroundMovement();
        // HandleJump();
    }

    public void HandleGroundMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ) * moveSpeed;
        move = transform.TransformDirection(move);  // Para que se mueva en la dirección en que el objeto está mirando

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);  // Mantener la velocidad en Y (gravedad)
    }

    public void HandleAirMovement()
    {
        // Mantener la inercia cuando está en el aire
        rb.velocity = new Vector3(jumpInertia.x, rb.velocity.y, jumpInertia.z);

        // Movimiento automático hacia adelante mientras salta
        if (Inertia)
        {
            Vector3 forwardMovement = transform.forward * moveSpeed; // Movimiento hacia adelante
            rb.velocity = new Vector3(forwardMovement.x, rb.velocity.y, forwardMovement.z); // Mantener la velocidad en Y (gravedad)
        }
    }

    public void HandleJump()
    {
        // Almacenar la velocidad en X/Z para mantener la inercia
        jumpInertia = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Aplicar fuerza de salto
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Inertia = true;
    }
}
