using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    bool isHit = false;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;
        IdentityHandler identityHandler = player.gameObject.GetComponent<IdentityHandler>();
        Debug.Log("Player hit: " + player.gameObject.name);
        AudioSource.PlayClipAtPoint(audioManager.enemiesHit[Random.Range(0, 1)], transform.position);
        identityHandler.RemoveIdentity();
        if (!isHit)
        {
            Destroy(transform.parent.gameObject);
            isHit = true;
        }
    }
}
