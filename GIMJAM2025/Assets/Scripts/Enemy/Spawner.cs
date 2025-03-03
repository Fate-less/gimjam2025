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
    public GameObject floorIntersectObject;
    public Transform floorIntersectOpenPos;
    public GameObject parentMonsterObject;
    private AudioManager audioManager;
    private IdentityHandler identityHandler;
    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        parentMonsterObject = GameObject.Find("Monsters");
        identityHandler = GameObject.Find("Game Handler").GetComponent<IdentityHandler>();
        Spawn();
    }
    public void Spawn()
    {
        if (parentMonsterObject == null)
        {
            parentMonsterObject = GameObject.Find("Monsters");
        }
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
            identityHandler.ReviveDeadPlayer();
            LeanTween.moveLocal(floorIntersectObject, floorIntersectOpenPos.position, 0.9f).setEaseOutQuad();
            Destroy(gameObject, 1f);
        }
    }
}
