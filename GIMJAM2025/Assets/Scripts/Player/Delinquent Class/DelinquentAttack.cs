using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelinquentAttack : Player, IAttacking
{
    [field: SerializeField] public float attackDuration {get; set;}
    [field: SerializeField] public float attackMoveDistance {get; set;}
    [field: SerializeField] public int attackDamage {get; set;}
    private bool isAttacking = false;
    private float attackTime = 0f;
    private Vector3 attackDirection;
    [field: SerializeField] public float knockbackDistance{get;set;}
    public Collider attackCollider;
    private PlayerMovement playerMovement;
    [field: SerializeField] float windUpDuration {get;set;}
    [field: SerializeField] float windUpDistance {get;set;}
    private float windUpTime;
    private bool attackDone, windUpDone;
    private Animator animator;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartAttack();
        }

        if (isAttacking)
        {         
            windUpTime += Time.deltaTime;
            attackTime += Time.deltaTime;
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
        playerMovement.enabled = false;
        animator.SetBool("isAttacking", true);
        attackDirection = playerMovement.GetMoveDirection();
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
        playerMovement.enabled = true;
        animator.SetBool("isAttacking", false);
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable == null) return;
        Debug.Log("Enemy hit: " + other.gameObject.name);
        damagable.TakeDamage(attackDamage);
        Rigidbody enemyRb = other.GetComponent<Rigidbody>();
        Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
        enemyRb.AddForce(knockbackDirection * knockbackDistance, ForceMode.Impulse);
    }
}
