using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelbstZerstörung : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            Invoke("Zerstören", 10f);
        }
    }

    void Zerstören()
    {
        Destroy(gameObject);
    }
}
