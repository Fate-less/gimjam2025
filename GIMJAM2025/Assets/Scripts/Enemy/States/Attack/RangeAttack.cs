using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State, IAttacking
{
    [field: Header("Stats")]
    [field: SerializeField] public float attackDuration { get; set; }
    [field: SerializeField] public float attackMoveDistance { get; set; }
    public int attackDamage { get; set; }
    public float knockbackDistance { get; set; }
    [field: Header("Referencing")]
    [SerializeField] GameObject magicBallObject;
    private float attackTime = 0f;
    private Vector3 attackDirection;
    private Transform player;
    private AudioManager audioManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        attackTime = 0;
    }

    void Update()
    {
        attackTime += Time.deltaTime;
        if (attackTime >= attackDuration)
        {
            StartAttack();
            attackTime = 0;
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    public void StartAttack()
    {
        attackDirection = (player.position - transform.position).normalized;
        AudioSource.PlayClipAtPoint(audioManager.rangeEnemiesKill, transform.position);
        Instantiate(magicBallObject, transform.position, Quaternion.LookRotation(attackDirection));
        Debug.Log("Magic");
        EndAttack();
    }

    public void EndAttack() { }
}
