using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayerMovement : Player
{
    [Header("Referencing")]
    public GameObject leftShadow;
    public GameObject rightShadow;
    private Animator leftShadowAnimator;
    private Animator rightShadowAnimator;
    void Start()
    {
        leftShadowAnimator = leftShadow.GetComponent<Animator>();
        rightShadowAnimator = rightShadow.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        if (moveX != 0 || moveZ != 0)
        {
            if (moveX < 0)
            {
                leftShadowAnimator.SetBool("isMoving", false);
                leftShadow.SetActive(false);
                rightShadow.SetActive(true);
                rightShadowAnimator.SetFloat("Horizontal", moveX);
            }
            else if (moveX > 0)
            {
                rightShadowAnimator.SetBool("isMoving", false);
                rightShadow.SetActive(false);
                leftShadow.SetActive(true);
                leftShadowAnimator.SetFloat("Horizontal", moveX);
            }
            if (rightShadow.activeInHierarchy)
            {
                rightShadowAnimator.SetFloat("Vertical", moveZ);
                rightShadowAnimator.SetBool("isMoving", true);
            }
            else
            {
                leftShadowAnimator.SetFloat("Vertical", moveZ);
                leftShadowAnimator.SetBool("isMoving", true);
            }
        }
        else 
        {
            if (rightShadow.activeInHierarchy) rightShadowAnimator.SetBool("isMoving", false);
            else leftShadowAnimator.SetBool("isMoving", false);
        }
    }
}

