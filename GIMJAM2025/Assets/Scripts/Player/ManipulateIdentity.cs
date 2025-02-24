using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ManipulateIdentity : Player
{
    public List<GameObject> playerObjects;
    public float ChangeIdentityCooldown;
    public GameObject DeadPlayer;
    private float CurrentChangeIdentityCooldown;
    void Start()
    { 
        CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
    }
    void Update()
    { 
        CurrentChangeIdentityCooldown-=Time.deltaTime;
        if(CurrentChangeIdentityCooldown <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
                SwapIdentity(1);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
                SwapIdentity(2);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
                SwapIdentity(3);
            }
        }
    }

    void SwapIdentity(int identity){
        if(playerObjects[identity-1] == DeadPlayer){
            return;
        }
        GameObject newIdentity = DeadPlayer;
        if(identity == 1)
        {
            if(gameObject != playerObjects[0])
            {
                newIdentity = Instantiate(playerObjects[0], transform.position, playerObjects[0].transform.rotation);
                RetrackVCam(newIdentity);
                Destroy(gameObject);
            }
        }
        else if(identity == 2)
        {
            if(gameObject != playerObjects[1])
            {
                newIdentity = Instantiate(playerObjects[1], transform.position, playerObjects[1].transform.rotation);
                RetrackVCam(newIdentity);
                Destroy(gameObject);
            }
        }
        else if(identity == 3)
        {
            if(gameObject != playerObjects[2])
            {
                newIdentity = Instantiate(playerObjects[2], transform.position, playerObjects[2].transform.rotation);
                RetrackVCam(newIdentity);
                Destroy(gameObject);
            }
        }
        try{
            ManipulateIdentity newIdentityManipulateIdentity = newIdentity.GetComponent<ManipulateIdentity>();
            for(int i=0;i<playerObjects.Count;i++){
                if(playerObjects[i] == DeadPlayer){
                    newIdentityManipulateIdentity.playerObjects.RemoveAt(i);
                    newIdentityManipulateIdentity.playerObjects.Insert(i, DeadPlayer);
                }
            }
        } catch {}
    }

    public void SplitIdentity()
    {
        if(gameObject == playerObjects[0])
        {
            GameObject newIdentity = Instantiate(playerObjects[1], transform.position, playerObjects[0].transform.rotation);
            RetrackVCam(newIdentity);
            
            playerObjects[0].GetComponent<PlayerStatus>().isDead = true;
            newIdentity.GetComponent<ManipulateIdentity>().playerObjects.RemoveAt(0);
            newIdentity.GetComponent<ManipulateIdentity>().playerObjects.Insert(0, DeadPlayer);
        }
        else if(gameObject == playerObjects[1])
        {
            GameObject newIdentity = Instantiate(playerObjects[2], transform.position, playerObjects[0].transform.rotation);
            RetrackVCam(newIdentity);
            playerObjects[1].GetComponent<PlayerStatus>().isDead = true;
            newIdentity.GetComponent<ManipulateIdentity>().playerObjects.RemoveAt(1);
            newIdentity.GetComponent<ManipulateIdentity>().playerObjects.Insert(1, DeadPlayer);
        }
        else if(gameObject == playerObjects[2])
        {
            GameObject newIdentity = Instantiate(playerObjects[0], transform.position, playerObjects[0].transform.rotation);
            RetrackVCam(newIdentity);
            playerObjects[2].GetComponent<PlayerStatus>().isDead = true;
            newIdentity.GetComponent<ManipulateIdentity>().playerObjects.RemoveAt(2);
            newIdentity.GetComponent<ManipulateIdentity>().playerObjects.Insert(2, DeadPlayer);
        }
    }

    private void RetrackVCam(GameObject newIdentity){
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().Follow = newIdentity.transform;
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().LookAt = newIdentity.transform;
    }
}
