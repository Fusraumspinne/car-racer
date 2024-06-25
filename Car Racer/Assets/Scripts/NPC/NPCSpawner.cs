using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation, transform);
    }
}
