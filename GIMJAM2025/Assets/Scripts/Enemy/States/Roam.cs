using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : State
{
    public float moveSpeed = 3f;
    public float roamRadius = 10f;
    public float roamTime = 3f;

    private Vector3 roamTarget;
    private float roamTimer;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Roaming();
    }

    void Roaming()
    {
        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0 || Vector3.Distance(transform.position, roamTarget) < 0.5f)
        {
            PickNewRoamTarget();
        }

        Vector3 direction = (roamTarget - transform.position).normalized;
        if(rb.velocity.magnitude <= moveSpeed)
        {
            rb.AddForce(direction * moveSpeed, ForceMode.Acceleration);
        }
    }

    void PickNewRoamTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle * roamRadius;
        roamTarget = new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);
        roamTimer = roamTime;
    }
}
