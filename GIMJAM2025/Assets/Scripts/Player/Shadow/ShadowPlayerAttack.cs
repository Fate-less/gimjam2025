using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayerAttack : Player
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
    public void StartAttacking()
    {

    }
}

