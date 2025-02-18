using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health {get;set;}

    public void TakeDamage(int damage)
    {
        Health -= damage;
        //got hit vfx
        //got hit sfx
    }

    void Start(){ }
    void Update(){ }
    
}
