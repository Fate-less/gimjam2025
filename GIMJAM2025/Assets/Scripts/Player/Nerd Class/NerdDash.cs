using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdDash : Player, IDashing
{
    [field: SerializeField] public float dashSpeed {get;set;}
    [field: SerializeField] public float dashDuration {get;set;}
    [field: SerializeField] public float dashCooldown {get;set;}
    public float dashTime {get;set;}
    [SerializeField] private LayerMask wallLayer;
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

    public void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        dashDirection = playerMovement.GetMoveDirection();
        Vector3 dashVelocity = dashDirection * dashSpeed;
        Vector3 teleportTarget = transform.position + dashDirection * dashSpeed;
        if (!Physics.Raycast(transform.position, dashDirection, dashSpeed, wallLayer))
        {
            transform.position = teleportTarget;
            Debug.Log("Teleported to: " + teleportTarget);
        }
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        playerMovement.enabled = false;
    }

    public void StopDash()
    {
        isDashing = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
        Invoke(nameof(ResetDashCooldown), dashCooldown);
        playerMovement.enabled = true;
    }

    void ResetDashCooldown()
    {
        dashTime = 0f;
    }
}