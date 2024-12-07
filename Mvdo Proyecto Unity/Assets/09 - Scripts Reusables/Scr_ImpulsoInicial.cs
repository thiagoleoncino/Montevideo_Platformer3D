using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ImpulsoInicial : MonoBehaviour
{
    public int velX;  // Velocidad en el eje X
    public int velY;  // Velocidad en el eje Y
    public int velZ;  // Velocidad en el eje Z

    [Space(10)]
    public bool boolRandomX;  // Activar/desactivar aleatorización para el eje X
    public int randomMaxValueX;
    public int randomMinValueX;

    [Space(10)]
    public bool boolRandomY;  // Activar/desactivar aleatorización para el eje Y
    public int randomMaxValueY;
    public int randomMinValueY;

    [Space(10)]
    public bool boolRandomZ;  // Activar/desactivar aleatorización para el eje Z
    public int randomMaxValueZ;
    public int randomMinValueZ;

    private Rigidbody rb;  // Referencia al Rigidbody del objeto

    // Start is called before the first frame update
    void Start()
    {
        // Obtiene la referencia al Rigidbody
        rb = GetComponent<Rigidbody>();

        // Verifica si el Rigidbody existe en el objeto
        if (rb != null)
        {
            // Asignar valores aleatorios si las variables booleanas están activadas
            if (boolRandomX)
            {
                velX = Random.Range(randomMinValueX, randomMaxValueX);  
            }

            if (boolRandomY)
            {
                velY = Random.Range(randomMinValueY, randomMaxValueY);  
            }

            if (boolRandomZ)
            {
                velZ = Random.Range(randomMinValueZ, randomMaxValueZ);  
            }

            // Aplica el impulso inicial
            rb.velocity = new Vector3(velX, velY, velZ);
        }
        else
        {
            Debug.LogError("No se encontró un Rigidbody en el objeto.");
        }
    }
}

