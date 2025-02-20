using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InChaseRange : Condition
{
    private GameObject ballObject;

    void Start()
    {
        ballObject = GameObject.FindGameObjectWithTag("Ball");
    }

    public override bool ConditionCheck()
    {
        if(ballObject.transform.position.x > 0)
        {
            return true;
        }
        return false;
    }
}
