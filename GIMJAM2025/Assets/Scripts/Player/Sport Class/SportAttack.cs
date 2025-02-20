using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportAttack : Player, IAttacking
{
    [field: SerializeField] public float attackDuration {get;set;}
    [field: SerializeField] public float attackMoveDistance {get;set;}
    [field: SerializeField] public int attackDamage {get;set;}
    private bool isAttacking = false;
    private float attackTime = 0f;
    private Vector3 attackDirection;
    public float knockbackDistance{get;set;}
    public Collider attackCollider;
    public GameObject SportSlashVFX;
    private PlayerMovement playerMovement;
    private Animator animator;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
            attackTime += Time.deltaTime;
            if (attackTime <= attackDuration)
            {
                transform.Translate(attackDirection * attackMoveDistance * Time.deltaTime);
            }
            else
            {
                EndAttack();
            }
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
        attackTime = 0f;
        playerMovement.enabled = false;
        animator.SetBool("isAttacking", true);
        attackDirection = playerMovement.GetMoveDirection();
        StartCoroutine(SlashVFX());
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }

    public void EndAttack()
    {
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

    IEnumerator SlashVFX()
    {
        yield return new WaitForSeconds(attackDuration / 3f);
        SportSlashVFX.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SportSlashVFX.SetActive(false);
    }
}
