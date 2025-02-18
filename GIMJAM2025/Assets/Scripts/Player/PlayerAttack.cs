using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f; // The range of the attack (1 tile in front of the player)
    public float attackDuration = 0.5f; // Duration of the attack animation
    public float attackMoveDistance = 0.2f; // Distance the attack moves forward with each swing
    public LayerMask enemyLayer; // Layer mask to check for enemies in the attack range

    private bool isAttacking = false;
    private float attackTime = 0f;
    private Vector3 attackDirection;

    private PlayerMovement playerMovement;

    // Reference to the player's Collider (for the hitbox)
    public Collider attackCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();

        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Disable hitbox by default
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Mouse left-click to trigger the attack
        {
            StartAttack();
        }

        if (isAttacking)
        {
            attackTime += Time.deltaTime;

            // Move attack hitbox forward during the swing
            if (attackTime <= attackDuration)
            {
                transform.parent.Translate(attackDirection * attackMoveDistance * Time.deltaTime);
            }
            else
            {
                EndAttack();
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        attackTime = 0f;

        // Disable player movement (by disabling movement script or flag)
        playerMovement.enabled = false;

        // Get the direction in front of the player based on their facing
        attackDirection = transform.forward; // Assuming forward is in the direction the player is facing

        // Enable the attack hitbox (Collider) for the attack duration
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }

    void EndAttack()
    {
        isAttacking = false;

        // Re-enable player movement after the attack
        playerMovement.enabled = true;

        // Disable the attack hitbox
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    // Optionally, you can detect enemies in the hitbox
    private void OnTriggerEnter(Collider other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            // Handle hit logic, such as damaging the enemy
            Debug.Log("Enemy hit: " + other.gameObject.name);
        }
    }
}
