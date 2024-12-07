using System.Collections;
using UnityEngine;

public class Scr_Crate : MonoBehaviour
{
    public GameObject objectToSpawn; // Objeto que se instanciar�
    public GameObject destroyEffect; // Efecto visual al destruir el crate
    public Transform spawnPoint; // Punto inicial para instanciar objetos
    public Transform effectSpawnPoint; // Punto inicial para instanciar objetos

    public int spawnCount = 1; // Cantidad de objetos a instanciar
    public float spawnOffset = 0.5f; // Distancia entre los objetos generados

    private bool hasSpawned = false; // Evitar m�ltiples generaciones

    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto que ha entrado en el trigger tiene el tag "PlayerHitboxTag"
        if (other.CompareTag("PlayerHitboxTag") && !hasSpawned)
        {
            hasSpawned = true; // Marca que ya se han instanciado los objetos

            // Instancia el efecto de destrucci�n
            if (destroyEffect != null)
                Instantiate(destroyEffect, effectSpawnPoint.position, spawnPoint.rotation);

            // Genera objetos uno al lado del otro en el eje X
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 offset = new Vector3(i * spawnOffset, 0, 0); // Desplazamiento horizontal
                Instantiate(objectToSpawn, spawnPoint.position + offset, spawnPoint.rotation);
            }

            Destroy(gameObject); // Destruye el crate despu�s de generar los objetos
        }
    }
}
