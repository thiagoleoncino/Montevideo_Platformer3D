using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Effect1 : MonoBehaviour
{
    public Transform cameraTransform; // Asigna la c�mara en el Inspector

    public float Var1;
    public float Var2;

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

    // Update is called once per frame
    void Update()
    {
        // Calcula la direcci�n hacia la c�mara
        Vector3 directionToCamera = cameraTransform.position - transform.position;

        // Calcula el �ngulo en Y para que el objeto mire a la c�mara
        Var2 = Mathf.Atan2(directionToCamera.x, directionToCamera.z) * Mathf.Rad2Deg;

        // Aplica la rotaci�n: Var1 para X, Var2 para Y, y rotaci�n adicional en Z
        transform.rotation = Quaternion.Euler(Var1, Var2, transform.rotation.eulerAngles.z + 100f * Time.deltaTime);
    }
}
