using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    bool isHit = false;
    private AudioManager audioManager;
    private IdentityHandler identityHandler;
    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        identityHandler = GameObject.Find("Game Handler").GetComponent<IdentityHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;
        Debug.Log("Player hit: " + player.gameObject.name);
        AudioSource.PlayClipAtPoint(audioManager.enemiesHit[Random.Range(0, 1)], transform.position);
        identityHandler.RemoveIdentity();
        if (!isHit)
        {
            Destroy(gameObject);
            isHit = true;
        }
    }
}
