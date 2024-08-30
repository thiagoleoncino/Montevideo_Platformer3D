using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_PlayerCamera : MonoBehaviour
{
    public Transform objetivo; // El objeto a seguir
    public float velocidad = 5f; // Velocidad de seguimiento
    public float distanciaMinima = 0.1f; // Distancia mínima al objetivo para dejar de moverse

    void LateUpdate()
    {
        if (objetivo != null)
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);

            if (distancia > distanciaMinima)
            {
                transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
            }
        }
    }

}
