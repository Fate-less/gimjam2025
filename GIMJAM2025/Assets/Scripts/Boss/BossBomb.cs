using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    [Header("Stats")]
    public float bombDamage;
    public float bombRange;
    [Header("Referencing")]
    public GameObject hitEffect;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("explode");
        Instantiate(hitEffect, transform.position, transform.rotation);
        BombExplode(transform.position, bombRange);
    }

    public void BombExplode(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
            {
                hitCollider.gameObject.GetComponent<IdentityHandler>().RemoveIdentity();
            }
        }
        AudioSource.PlayClipAtPoint(audioManager.boss3Abilities, transform.position);
        Destroy(gameObject);
    }
}
