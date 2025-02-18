using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : Player
{
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private float dashTime = 0f;
    private bool isDashing = false;
    private Vector3 dashDirection;

    private PlayerMovement playerMovement;
    private TrailRenderer trailRenderer;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        trailRenderer = GetComponent<TrailRenderer>();

        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashTime <= 0f)
        {
            StartDash();
        }
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                StopDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        dashDirection = playerMovement.GetMoveDirection();
        Vector3 dashVelocity = dashDirection * dashSpeed;
        GetComponent<Rigidbody>().velocity = dashVelocity;
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
    }

    void StopDash()
    {
        isDashing = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
        Invoke(nameof(ResetDashCooldown), dashCooldown);
    }

    void ResetDashCooldown()
    {
        dashTime = 0f;
    }
}
