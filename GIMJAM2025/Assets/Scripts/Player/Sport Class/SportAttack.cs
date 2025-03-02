using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportAttack : Player, IAttacking
{
    [field: Header("Stats")]
    [field: SerializeField] public float attackDuration {get;set;}
    [field: SerializeField] public float attackMoveDistance {get;set;}
    [field: SerializeField] public int attackDamage {get;set;}
    private bool isAttacking = false;
    private float attackTime = 0f;
    private Vector3 attackDirection;
    [field: SerializeField] public float knockbackDistance {get;set;}
    [field: Header("Referencing")]
    public Collider attackCollider;
    public GameObject SportSlashVFX;
    public GameObject hitEffectObject;
    private PlayerMovement playerMovement;
    private Animator animator;
    private AudioManager audioManager;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
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
        AudioSource.PlayClipAtPoint(audioManager.sportHit, transform.position);
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
        if (!other.CompareTag("Enemy"))
        {
            return;
        }
        GameObject enemyObject = other.transform.parent.gameObject;
        IDamagable damagable = enemyObject.GetComponent<IDamagable>();
        if (damagable == null) return;
        Debug.Log("Enemy hit: " + enemyObject.gameObject.name);
        damagable.TakeDamage(attackDamage);
        Instantiate(hitEffectObject, enemyObject.transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(audioManager.enemiesHit[1],enemyObject.transform.position);
        Rigidbody enemyRb = enemyObject.GetComponent<Rigidbody>();
        Vector3 knockbackDirection = (enemyObject.transform.position - transform.position).normalized;
        enemyRb.AddForce(knockbackDirection * knockbackDistance, ForceMode.Impulse);
        Debug.Log("enemy knockbacked");
    }

    IEnumerator SlashVFX()
    {
        yield return new WaitForSeconds(attackDuration / 3f);
        SportSlashVFX.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SportSlashVFX.SetActive(false);
    }
}
