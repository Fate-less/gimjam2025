using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    bool isHit = false;
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject playerObject = other.transform.parent.gameObject;
        ManipulateIdentity playerIdentity = playerObject.GetComponent<ManipulateIdentity>();
        if (playerIdentity == null) return;
        Debug.Log("Player hit: " + playerObject.gameObject.name);
        playerIdentity.SplitIdentity();
        if(!isHit)
        {
            Destroy(transform.parent.gameObject);
            isHit = true;
        }
    }
}
