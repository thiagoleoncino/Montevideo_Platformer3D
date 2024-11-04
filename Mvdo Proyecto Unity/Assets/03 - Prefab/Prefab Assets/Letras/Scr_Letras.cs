using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Letras : MonoBehaviour
{
    public bool rotationY;
    public bool rotationZ;

    // Update is called once per frame
    void Update()
    {
        if(rotationY)
        {
            transform.Rotate(0,- 100f * Time.deltaTime, 0);

        }

        if (rotationZ)
        {
            transform.Rotate(0, 0,100f * Time.deltaTime);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDetection"))
        {
            Destroy(gameObject);
        }
    }
}
