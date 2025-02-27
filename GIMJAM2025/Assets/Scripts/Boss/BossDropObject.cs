using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropObject : MonoBehaviour
{
    [Header("Stats")]
    public int bombNumber;
    [Header("Referencing")]
    public GameObject bombObject;
    public Transform[] spawnPos;
    private AudioManager audioManager;
    void Start()
    {
        Spawn();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void Spawn()
    {
        AudioSource.PlayClipAtPoint(audioManager.boss3Cast, transform.position);
        for (int i = 0; i < bombNumber; i++)
        {
            int randomPos = Random.Range(0, spawnPos.Length - 1);
            Instantiate(bombObject, spawnPos[randomPos]);
        }
        Destroy(gameObject, 5f);
    }
}
