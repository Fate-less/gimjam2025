using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyObject;
    public int enemyNumber;
    public Transform[] spawnPos;
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        int enemyObjectInLine = 0;
        for(int i = 0; i < enemyNumber; i++)
        {
            int randomPos = Random.Range(0, spawnPos.Length-1);
            if(enemyObjectInLine > enemyObject.Length)
            {
                enemyObjectInLine = 0;
            }
            while(spawnPos[randomPos].childCount > 0){
                randomPos++;
            }
            Instantiate(enemyObject[enemyObjectInLine], spawnPos[randomPos]);
            enemyObjectInLine++;
        }
    }
}
