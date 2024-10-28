using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Crate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto que ha entrado en el trigger tiene el tag "Player"
        if (other.CompareTag("PlayerHitboxTag"))
        {
            Destroy(gameObject);
        }

    }
}
