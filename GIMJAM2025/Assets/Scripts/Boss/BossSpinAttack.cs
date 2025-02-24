using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpinAttack : MonoBehaviour
{
    public GameObject magicBall;
    public float rotationSpeed = 360f; // Degrees per second
    public float duration = 1f; // Time to complete one full rotation
    private float attackDelay;
    private AudioManager audioManager;

    private float timer = 0f;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AudioSource.PlayClipAtPoint(audioManager.boss2Abilities, transform.position);
    }
    void Update()
    {
        // Rotate the object
        float rotationStep = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationStep, 0f);
        attackDelay += Time.deltaTime;
        float randomValue = Random.Range(0.1f, 0.5f);
        if(attackDelay>randomValue){
            Instantiate(magicBall, transform.position, transform.rotation);
            attackDelay=0;
        }
        
        // Track the time elapsed
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}

