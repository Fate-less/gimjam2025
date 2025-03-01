using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicBall : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 10f;
    public float lifetime = 3f;
    public float damage = 10f;
    [Header("Referencing")]
    public GameObject hitEffectObject;
    private AudioManager audioManager;
    private IdentityHandler identityHandler;

    void Start()
    {
        Destroy(gameObject, lifetime);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        identityHandler = GameObject.Find("Game Handler").GetComponent<IdentityHandler>();
        AudioSource.PlayClipAtPoint(audioManager.nerdHit, transform.position);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (player == null) return;
        identityHandler.RemoveIdentity();
        Debug.Log("Player hit: " + player.gameObject.name);
        Destroy(gameObject);
    }
}
