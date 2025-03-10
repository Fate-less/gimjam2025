using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [field: Header("Stats")]
    [field: SerializeField] public int Health { get; set; }

    public void TakeDamage(int damage)
    {
        GetComponent<DamageFlash>().Flash();
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Start() { }
    void Update() { }
}
