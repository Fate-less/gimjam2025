using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdDash : Player, IDashing
{
    [field: Header("Nerd Stats")]
    [field: SerializeField] public float dashSpeed { get; set; }
    [field: SerializeField] public float dashDuration { get; set; }
    [field: SerializeField] public float dashCooldown { get; set; }
    public float dashTime { get; set; }
    [SerializeField] private LayerMask prohibitedLayer;
    private bool isDashing = false;
    private Vector3 dashDirection;
    private PlayerMovement playerMovement;
    private TrailRenderer trailRenderer;
    private AudioManager audioManager;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        trailRenderer = GetComponent<TrailRenderer>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

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
        AudioSource.PlayClipAtPoint(audioManager.nerdAbilities, transform.position);
        Vector3 teleportTarget = transform.position + dashDirection * dashSpeed;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dashDirection, out hit, dashSpeed, prohibitedLayer))
        {
            teleportTarget = hit.point;
            Debug.Log("Hit wall, stopping at: " + teleportTarget);
        }
        transform.position = teleportTarget;
        playerMovement.enabled = false;
    }

    public void StopDash()
    {
        isDashing = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Invoke(nameof(ResetDashCooldown), dashCooldown);
        playerMovement.enabled = true;
    }

    void ResetDashCooldown()
    {
        dashTime = 0f;
    }
}