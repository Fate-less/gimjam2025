using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    [Header("Referencing")]
    public GameObject spawnerObject;
    private AudioSource audioSource;
    private AudioManager audioManager;
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.clip = audioManager.map1Battle;
            spawnerObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
