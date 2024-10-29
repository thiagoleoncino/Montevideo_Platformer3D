using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Crate : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto que ha entrado en el trigger tiene el tag "Player"
        if (other.CompareTag("PlayerHitboxTag"))
        {
            Instantiate(objectToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Destroy(gameObject);
        }

    }
}
