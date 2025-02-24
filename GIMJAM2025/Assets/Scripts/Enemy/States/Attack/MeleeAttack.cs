using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : State, IAttacking
{
    [field: SerializeField] public float attackDuration {get; set;}
    [field: SerializeField] public float attackMoveDistance {get; set;}
    public int attackDamage {get; set;}
    private bool isAttacking = false;
    private float attackTime = 0f;
    private Vector3 attackDirection;
    public float knockbackDistance{get;set;}
    public Collider attackCollider;
    private Transform player;
    [field: SerializeField] float windUpDuration {get;set;}
    [field: SerializeField] float windUpDistance {get;set;}
    private float windUpTime;
    private bool attackDone, windUpDone;
    bool isHit = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    void Update()
    {
        if (!isAttacking)
        {
            StartAttack();
        }

        if (isAttacking)
        {         
            windUpTime += Time.deltaTime;
            attackTime += Time.deltaTime;
        }
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }
    void FixedUpdate()
    {
        if(!windUpDone)
        {
            GetComponent<Rigidbody>().AddForce(-attackDirection * windUpDistance, ForceMode.Impulse);
            Debug.Log("Windup");
            windUpDone = true;
        }
        if(windUpTime >= windUpDuration && !attackDone)
        {
            GetComponent<Rigidbody>().AddForce(attackDirection * attackMoveDistance, ForceMode.Impulse);
            Debug.Log("Attack");
            windUpTime = 0f;
            attackDone = true;
        }
        if (attackTime >= attackDuration + windUpDuration)
        {
            attackTime = 0f;
            EndAttack();
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
        attackTime = 0f; windUpTime = 0f;
        windUpDone = false; attackDone = false;
        attackDirection = (player.position - transform.position).normalized;
        attackDirection.y = 0;
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }

    public void EndAttack()
    {
        Debug.Log("Attack stopped");
        isAttacking = false;
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject playerObject = other.gameObject;
        ManipulateIdentity playerIdentity = playerObject.GetComponent<ManipulateIdentity>();
        if (playerIdentity == null) return;
        Debug.Log("Player hit: " + playerObject.gameObject.name);
        playerIdentity.SplitIdentity();
        if (!isHit)
        {
            Destroy(gameObject);
            isHit = true;
        }
    }
}
