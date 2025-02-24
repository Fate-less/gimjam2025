using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicBall : MonoBehaviour
{
    public float speed = 10f;
    bool isHit = false;
    public float lifetime = 3f;
    public float damage = 10f;
    public GameObject hitEffectObject;
    private AudioManager audioManager;

    void Start()
    {
        Destroy(gameObject, lifetime);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AudioSource.PlayClipAtPoint(audioManager.nerdHit, transform.position);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject playerObject = other.gameObject;
            if(playerObject.GetComponent<PlayerStatus>().isDead){
                return;
            }
            ManipulateIdentity playerIdentity = playerObject.GetComponent<ManipulateIdentity>();
            if (playerIdentity == null) return;
            Debug.Log("Player hit: " + playerObject.gameObject.name);
            playerIdentity.SplitIdentity();
            if (!isHit)
            {
                Destroy(gameObject);
                isHit = true;
            }
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
