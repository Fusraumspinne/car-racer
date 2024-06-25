using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDestroyer : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Invoke("Zerstören", 5f);
        }
    }

    void Zerstören()
    {
        Destroy(gameObject);
    }
}
