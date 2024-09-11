using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Prueba : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed = 10f; // Velocidad de rotación de la cámara
    private bool isColliding = false;

    void Start()
    {
        // Asegúrate de que el CinemachineCollider esté configurado
        if (virtualCamera != null)
        {
            CinemachineCollider collider = virtualCamera.GetComponent<CinemachineCollider>();
            if (collider == null)
            {
                Debug.LogError("No se encontró CinemachineCollider en la cámara virtual.");
            }
        }
    }

    void Update()
    {
        // Si está colisionando, rota la cámara en el eje Y
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
        // Aplica una rotación suave en el eje Y
        virtualCamera.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
