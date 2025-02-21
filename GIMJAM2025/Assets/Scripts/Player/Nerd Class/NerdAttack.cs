using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdAttack : Player, IAttacking
{
    [field: SerializeField] public float attackDuration {get;set;}
    [field: SerializeField] public float attackMoveDistance {get;set;}
    public int attackDamage {get;set;}
    [SerializeField] GameObject magicBallObject;
    private bool isAttacking = false;
    private bool castingDone = true;
    private float attackTime = 0f;
    private Vector3 attackDirection;
    [SerializeField] float windUpDuration;
    public float knockbackDistance{get;set;}
    private float windUpTime;
    private PlayerMovement playerMovement;
    private Animator animator;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
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
            if (attackTime >= attackDuration)
            {
                EndAttack();
            }
        }
    }

    void FixedUpdate()
    {
        if (!castingDone)
        {
            if(windUpTime >= windUpDuration)
            {
                GetComponent<Rigidbody>().AddForce(-attackDirection * attackMoveDistance, ForceMode.Impulse);
                Instantiate(magicBallObject, transform.position, Quaternion.LookRotation(attackDirection));
                Debug.Log("Magic");
                castingDone = true;
                windUpTime = 0;
            }
        }
    }

    public void StartAttack()
    {
        castingDone = false;
        isAttacking = true;
        attackTime = 0f;
        playerMovement.enabled = false;
        animator.SetBool("isAttacking", true);
        // Calculate attack direction from mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            attackDirection = (hit.point - transform.position).normalized;
            attackDirection.y = 0; // Keep the direction on the XZ plane
        }
        else
        {
            attackDirection = transform.forward; // Default to facing forward if no valid hit
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        playerMovement.enabled = true;
        animator.SetBool("isAttacking", false);
    }
}