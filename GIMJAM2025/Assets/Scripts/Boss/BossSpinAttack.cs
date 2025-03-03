using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpinAttack : MonoBehaviour
{
    [Header("Stats")]
    public float rotationSpeed = 360f;
    public float duration = 1f;
    [Header("Referencing")]
    public GameObject magicBall;
    private float attackDelay;
    private AudioManager audioManager;
    private float timer = 0f;
    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        AudioSource.PlayClipAtPoint(audioManager.boss2Abilities, transform.position);
    }
    void Update()
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationStep, 0f);
        attackDelay += Time.deltaTime;
        float randomValue = Random.Range(0.1f, 0.5f);
        if (attackDelay > randomValue)
        {
            Instantiate(magicBall, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
            attackDelay = 0;
        }
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}

