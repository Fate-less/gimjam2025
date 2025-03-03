using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayerMovement : Player
{
    [Header("Referencing")]
    public GameObject shadowObject;
    private Animator shadowAnimator;
    void Start()
    {
        shadowAnimator = shadowObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        if (moveX != 0 || moveZ != 0)
        {
            if (moveX < 0)
            {
                shadowAnimator.SetBool("isMoving", false);
                shadowAnimator.SetFloat("Horizontal", moveX);
            }
            else if (moveX > 0)
            {
                shadowAnimator.SetBool("isMoving", false);
                shadowAnimator.SetFloat("Horizontal", moveX);
            }
            shadowAnimator.SetFloat("Vertical", moveZ);
            shadowAnimator.SetBool("isMoving", true);
        }
        else 
        {
            shadowAnimator.SetBool("isMoving", false);
        }
    }
}

