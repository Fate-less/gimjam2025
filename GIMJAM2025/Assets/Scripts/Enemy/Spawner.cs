using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyObject;
    public int enemyNumber;
    public Transform[] spawnPos;

    public void Spawn()
    {
        int randomPos = Random.Range(0, spawnPos.Length-1);
        int enemyObjectInLine = 0;
        for(int i = 0; i < enemyNumber; i++)
        {
            if(enemyObjectInLine < enemyObject.Length)
            {
                Instantiate(enemyObject[enemyObjectInLine], spawnPos[randomPos]);
                enemyObjectInLine++;
            }
            else
            {
                enemyObjectInLine = 0;
            }
        }
    }
}
