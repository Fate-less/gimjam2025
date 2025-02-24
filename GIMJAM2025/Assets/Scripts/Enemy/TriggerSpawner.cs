using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    public GameObject spawnerObject;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            spawnerObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
