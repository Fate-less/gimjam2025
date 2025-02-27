using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossWall : MonoBehaviour
{
    [Header("Referencing")]
    public GameObject bossWall;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossWall.SetActive(true);
            Destroy(gameObject);
        }
    }
}
