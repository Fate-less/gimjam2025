using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    private Transform player;  // Assign the player's Transform in the inspector
    public float moveSpeed = 3f;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        ChasePlayer();
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if(rb.velocity.magnitude <= moveSpeed)
        {
            rb.AddForce(direction * moveSpeed, ForceMode.VelocityChange);
        }
    }
}
