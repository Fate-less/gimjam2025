using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPhase : Enemy, IDamagable
{
    [Header("Stats")]
    public float spawnMobsCooldown;
    public float spinAttackCooldown;
    public float bombAttackCooldown;
    [Header("Referencing")]
    public GameObject BOSS_SpawnMob;
    public GameObject BOSS_SpinAttack;
    public GameObject BOSS_BombAttack;
    private float spawnMobsTimer;
    private float spinAttackTimer;
    private float bombAttackTimer;
    private float timeFlee;
    private AudioSource audioSource;
    private AudioManager audioManager;
    private Animator animator;
    private float phase;
    private float currentPhase;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timeFlee += Time.deltaTime;

        spawnMobsTimer += Time.deltaTime;
        if (spawnMobsTimer >= spawnMobsCooldown)
        {
            Debug.Log("Mobs Spawned");
            Instantiate(BOSS_SpawnMob, new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity);
            spawnMobsTimer = 0f;
        }

        if (Health <= 70)
        {
            spinAttackTimer += Time.deltaTime;
            if (spinAttackTimer >= spinAttackCooldown)
            {
                Instantiate(BOSS_SpinAttack, transform.position, Quaternion.identity);
                spinAttackTimer = 0f;
                phase = 2;
            }
        }

        if (Health <= 40)
        {
            bombAttackTimer += Time.deltaTime;
            if (bombAttackTimer >= bombAttackCooldown)
            {
                Instantiate(BOSS_BombAttack, transform.position, Quaternion.identity);
                bombAttackTimer = 0f;
                phase = 3;
            }
        }
        if (currentPhase != phase)
        {
            ChangeBGM(Health);
        }
        if (Health <= 10)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public void ChangeBGM(float health)
    {
        if (health < 70 && health > 40)
        {
            animator.SetInteger("Phase", 2);
            audioSource.clip = audioManager.boss2BGM;
            audioSource.Play();
            currentPhase = 2;
        }
        if (health < 40)
        {
            animator.SetInteger("Phase", 3);
            audioSource.clip = audioManager.boss3BGM;
            audioSource.Play();
            currentPhase = 3;
        }
        if (Health <= 10)
        {
            SceneManager.LoadScene("Ending");
        }
    }
}