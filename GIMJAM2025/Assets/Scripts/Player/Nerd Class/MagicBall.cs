using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public float damage = 10f;
    public float knockbackForce = 5f;
    public GameObject hitEffectObject;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject enemyObject = other.transform.parent.gameObject;
        IDamagable damagable = enemyObject.GetComponent<IDamagable>();
        
        if (enemyObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = enemyObject.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 knockbackDirection = (enemyObject.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                Instantiate(hitEffectObject, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
