using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelinquentDash : Player, IDashing
{
    [field: Header("Stats")]
    [field: SerializeField] public float dashSpeed {get;set;}
    [field: SerializeField] public float dashDuration {get;set;}
    [field: SerializeField] public float dashCooldown {get;set;}
    public float dashTime {get;set;}
    private bool isDashing = false;
    private Vector3 dashDirection;
    private Vector3 dashVelocity;
    private float dashCurrentCooldown;
    private PlayerMovement playerMovement;
    private TrailRenderer trailRenderer;
    private AudioManager audioManager;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        trailRenderer = GetComponent<TrailRenderer>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
    }

    void Update()
    {
        dashCurrentCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCurrentCooldown <= 0f)
        {
            StartDash();
        }
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (dashTime <= 0f && isDashing)
        {
            GetComponent<Rigidbody>().AddForce(dashVelocity, ForceMode.Impulse);
            StopDash();
        }
    }

    public void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
        dashDirection = playerMovement.GetMoveDirection();
        dashDirection.y = 1;
        dashVelocity = dashDirection * dashSpeed;
        AudioSource.PlayClipAtPoint(audioManager.bullyAbilities, transform.position);
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
        playerMovement.enabled = true;
        dashCurrentCooldown = dashCooldown;
    }
}
