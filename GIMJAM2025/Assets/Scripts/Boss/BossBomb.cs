using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    public float bombDamage;
    public float bombRange;
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

    public void BombExplode(Vector3 center, float radius){
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag == "Player"){
                hitCollider.gameObject.GetComponent<ManipulateIdentity>().SplitIdentity();
            }
        }
        AudioSource.PlayClipAtPoint(audioManager.boss3Abilities,transform.position);
        Destroy(gameObject);
    }
}
