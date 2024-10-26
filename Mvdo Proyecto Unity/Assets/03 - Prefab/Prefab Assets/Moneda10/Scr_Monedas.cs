using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Monedas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotar el objeto en el eje Y
        transform.Rotate(0, 0, 100f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDetection"))
        {
            Destroy(gameObject);
        }
    }
}
