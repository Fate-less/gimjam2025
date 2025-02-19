using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacking
{
    float attackDuration {get;set;}
    float attackMoveDistance {get;set;}
    int attackDamage {get;set;}
    float knockbackDistance {get;set;}

    void StartAttack();
    void EndAttack();
}
