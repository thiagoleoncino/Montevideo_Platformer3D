using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_SeguirCamara : MonoBehaviour
{
    private Transform cameraTransform; // Asigna la c�mara en el Inspector

    private void Start()
    {
        // Busca el objeto con el nombre "VirtualCamara" y asigna su transform a cameraTransform
        GameObject virtualCameraObject = GameObject.Find("VirtualCamara");
        if (virtualCameraObject != null)
        {
            cameraTransform = virtualCameraObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� un objeto llamado 'VirtualCamara'.");
        }
    }

    void Update()
    {
        // Hacer que el objeto mire hacia la c�mara
        transform.LookAt(cameraTransform);
    }
}
