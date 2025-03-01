using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 10f;
    public float lifetime = 3f;
    public float damage = 10f;
    public float knockbackForce = 5f;
    [Header("Referencing")]
    public GameObject hitEffectObject;
    private AudioManager audioManager;

    void Start()
    {
        Destroy(gameObject, lifetime);
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        AudioSource.PlayClipAtPoint(audioManager.nerdHit, transform.position);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemyObject = other.transform.parent.gameObject;
            IDamagable damagable = enemyObject.GetComponent<IDamagable>();
            if (damagable == null) return;
            Debug.Log("Enemy hit: " + enemyObject.gameObject.name);
            damagable.TakeDamage((int)damage);
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
