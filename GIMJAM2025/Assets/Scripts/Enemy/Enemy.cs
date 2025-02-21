using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [field: SerializeField] public int Health { get; set; }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health<=0)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
    }
}
