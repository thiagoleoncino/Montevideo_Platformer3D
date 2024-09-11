using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Prueba : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed = 10f; // Velocidad de rotaci�n de la c�mara
    private bool isColliding = false;

    void Start()
    {
        // Aseg�rate de que el CinemachineCollider est� configurado
        if (virtualCamera != null)
        {
            CinemachineCollider collider = virtualCamera.GetComponent<CinemachineCollider>();
            if (collider == null)
            {
                Debug.LogError("No se encontr� CinemachineCollider en la c�mara virtual.");
            }
        }
    }

    void Update()
    {
        // Si est� colisionando, rota la c�mara en el eje Y
        if (isColliding)
        {
            RotateCamera();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si colisiona con un objeto con la etiqueta "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Cuando deje de colisionar
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = false;
        }
    }

    private void RotateCamera()
    {
        // Aplica una rotaci�n suave en el eje Y
        virtualCamera.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
