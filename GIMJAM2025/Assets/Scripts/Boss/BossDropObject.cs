using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropObject : MonoBehaviour
{
    public GameObject bombObject;
    public int bombNumber;
    public Transform[] spawnPos;
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        for(int i = 0; i < bombNumber; i++)
        {
            int randomPos = Random.Range(0, spawnPos.Length-1);
            Instantiate(bombObject, spawnPos[randomPos]);
        }
        Destroy(gameObject,5f);
    }
}
