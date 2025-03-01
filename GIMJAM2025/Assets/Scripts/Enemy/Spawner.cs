using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Stats")]
    public int enemyNumber;
    [Header("Referencing")]
    public GameObject[] enemyObject;
    public Transform[] spawnPos;
    public GameObject BattleWall;
    public GameObject parentMonsterObject;
    private AudioSource audioSource;
    private AudioManager audioManager;
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        BattleWall = GameObject.Find("Battle Wall");
        parentMonsterObject = GameObject.Find("Monsters");
        Spawn();
    }
    public void Spawn()
    {
        if (BattleWall == null)
        {
            BattleWall = GameObject.Find("Boss Wall");
        }
        if (parentMonsterObject == null)
        {
            parentMonsterObject = GameObject.Find("Monsters");
        }
        BattleWall.SetActive(true);
        int enemyObjectInLine = 0;
        for (int i = 0; i < enemyNumber; i++)
        {
            int randomPos = Random.Range(0, spawnPos.Length - 1);
            if (enemyObjectInLine > enemyObject.Length - 1)
            {
                enemyObjectInLine = 0;
            }
            while (spawnPos[randomPos].childCount > 0)
            {
                randomPos++;
            }
            GameObject monster = Instantiate(enemyObject[enemyObjectInLine], spawnPos[randomPos]);
            Debug.Log("enemyObjectInLine: " + enemyObjectInLine);
            Debug.Log("randomPos: " + randomPos);
            monster.transform.SetParent(parentMonsterObject.transform);
            enemyObjectInLine++;
        }
        AudioSource.PlayClipAtPoint(audioManager.enemiesSpawn, transform.position);
    }
    void Update()
    {
        if (parentMonsterObject.transform.childCount == 0)
        {
            BattleWall.SetActive(false);
            audioSource.clip = audioManager.map1Base;
            Destroy(gameObject);
        }
    }
}
