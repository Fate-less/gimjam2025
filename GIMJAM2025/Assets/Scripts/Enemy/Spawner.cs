using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyObject;
    public int enemyNumber;
    public Transform[] spawnPos;
    public GameObject BattleWall;
    public GameObject parentMonsterObject;
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        BattleWall.SetActive(true);
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
            GameObject monster = Instantiate(enemyObject[enemyObjectInLine], spawnPos[randomPos]);
            monster.transform.SetParent(parentMonsterObject.transform);
            enemyObjectInLine++;
        }
    }
    void Update()
    {
        if(parentMonsterObject.transform.childCount == 0){
            BattleWall.SetActive(false);
            Destroy(gameObject);
        }
    }
}
