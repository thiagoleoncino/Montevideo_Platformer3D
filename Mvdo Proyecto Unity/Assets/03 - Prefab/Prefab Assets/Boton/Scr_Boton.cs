using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Boton : MonoBehaviour
{
    public Transform pressurePlate;
    private float initialPosition;
    public float position;
    public bool press;

    private void Start()
    {
        initialPosition = pressurePlate.position.y;
        position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Actualizamos la posición y de pressurePlate
        pressurePlate.position = new Vector3(pressurePlate.position.x, position, pressurePlate.position.z);

        if (press)
        {
            position = initialPosition - 0.15f;  
        }
        else
        {
            position = initialPosition;  
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            press = true;
        }

    }
}
