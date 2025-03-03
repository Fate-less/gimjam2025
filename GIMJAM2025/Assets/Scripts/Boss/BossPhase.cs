using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPhase : Enemy, IDamagable
{
    [Header("Stats")]
    public float spinAttackCooldown;
    public float bombAttackCooldown;
    [Header("Referencing")]
    public GameObject BOSS_SpinAttack;
    public GameObject BOSS_BombAttack;
    private float spinAttackTimer;
    private float bombAttackTimer;
    private AudioSource audioSource;
    private AudioManager audioManager;
    private Animator animator;
    private float phase;
    private float currentPhase;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (Health <= 90)
        {
            phase = 2;
            spinAttackTimer += Time.deltaTime;
            if (spinAttackTimer >= spinAttackCooldown)
            {
                Instantiate(BOSS_SpinAttack, transform.position, Quaternion.identity);
                spinAttackTimer = 0f;
            }
        }

        if (Health <= 50)
        {
            phase = 3;
            bombAttackTimer += Time.deltaTime;
            if (bombAttackTimer >= bombAttackCooldown)
            {
                Instantiate(BOSS_BombAttack, transform.position, Quaternion.identity);
                bombAttackTimer = 0f;
            }
        }
        if (currentPhase != phase)
        {
            ChangeBGM(Health);
        }
        if (Health <= 5)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public void ChangeBGM(float health)
    {
        if (health < 90 && health > 50)
        {
            animator.SetInteger("Phase", 2);
            audioSource.clip = audioManager.boss2BGM;
            audioSource.Play();
            currentPhase = 2;
        }
        if (health < 50)
        {
            animator.SetInteger("Phase", 3);
            audioSource.clip = audioManager.boss3BGM;
            audioSource.Play();
            currentPhase = 3;
        }
    }
}