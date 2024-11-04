using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AgarrarItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDetection"))
        {
            Destroy(gameObject);
        }
    }
}
