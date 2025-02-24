using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    bool isHit = false;
    float countDown = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countDown += Time.deltaTime;
            if(countDown>=2){
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
                    Destroy(transform.parent.gameObject);
                    isHit = true;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        countDown = 0;
    }
}
