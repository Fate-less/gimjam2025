using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InChaseRange : Condition
{
    private Transform player;
    public float detectionRange = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override bool ConditionCheck()
    {
        try
        {
            if(player==null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        } catch {player = GameObject.FindGameObjectWithTag("Player").transform;}
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
