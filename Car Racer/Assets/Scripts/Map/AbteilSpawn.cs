using System.Collections;
using UnityEngine;

public class PeriodicSpawn : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval; // Intervall in Sekunden
    public float spawnRange; // Bereich, um den sich die x-Koordinate ändert
    public int spawnedObj;

    private void Start()
    {
        // Starte die Coroutine für das periodische Spawning
        StartCoroutine(SpawnObjectPeriodically());
    }

    IEnumerator SpawnObjectPeriodically()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        // Berechne die neue Spawn-Koordinate mit einer Verschiebung um 'spawnRange' in z-Richtung
        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + spawnedObj * spawnRange
        );

        // Instanziiere das objectToSpawn an den berechneten Koordinaten und ohne Rotation
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        spawnedObj++;
    }
}
