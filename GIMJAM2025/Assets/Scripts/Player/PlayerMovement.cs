using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    public float moveSpeed = 5f;
    private Vector3 moveDirection;
    private Animator animator;
    private Rigidbody rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        if(moveX != 0 || moveZ != 0)
        {
            animator.SetBool("isMoving", true);
            if(moveX < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (moveX > 0)
            { gameObject.GetComponent<SpriteRenderer>().flipX = false; }
        } else { animator.SetBool("isMoving", false); }
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

}

