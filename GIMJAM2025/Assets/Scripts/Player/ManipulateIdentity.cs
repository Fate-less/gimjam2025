using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ManipulateIdentity : Player
{
    public List<GameObject> playerObjects;
    public float ChangeIdentityCooldown;
    private float CurrentChangeIdentityCooldown;
    void Start()
    { 
        CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
    }
    void Update()
    { 
        if(CurrentChangeIdentityCooldown <= 0)
        {
            CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwapIdentity(1);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwapIdentity(2);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwapIdentity(3);
            }
        }
    }

    void SwapIdentity(int identity){
        if(identity == 1)
        {
            if(gameObject != playerObjects[0])
            {
                GameObject newIdentity = Instantiate(playerObjects[0], transform.position, playerObjects[0].transform.rotation);
                GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().Follow = newIdentity.transform;
                GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().LookAt = newIdentity.transform;
                Destroy(gameObject);
            }
        }
        else if(identity == 2)
        {
            if(gameObject != playerObjects[1])
            {
                GameObject newIdentity = Instantiate(playerObjects[1], transform.position, playerObjects[0].transform.rotation);
                GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().Follow = newIdentity.transform;
                GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().LookAt = newIdentity.transform;
                Destroy(gameObject);
            }
        }
        else if(identity == 3)
        {
            if(gameObject != playerObjects[2])
            {
                GameObject newIdentity = Instantiate(playerObjects[2], transform.position, playerObjects[0].transform.rotation);
                GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().Follow = newIdentity.transform;
                GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().LookAt = newIdentity.transform;
                Destroy(gameObject);
            }
        }
    }

    public void SplitIdentity()
    {
        if(gameObject != playerObjects[0])
        {
            GameObject newIdentity = Instantiate(playerObjects[1], transform.position, playerObjects[0].transform.rotation);
            playerObjects.RemoveAt(1);
        }
        else if(gameObject != playerObjects[1])
        {
            GameObject newIdentity = Instantiate(playerObjects[2], transform.position, playerObjects[0].transform.rotation);
            playerObjects.RemoveAt(2);
        }
        else if(gameObject != playerObjects[2])
        {
            GameObject newIdentity = Instantiate(playerObjects[0], transform.position, playerObjects[0].transform.rotation);
            playerObjects.RemoveAt(0);
        }
    }
}
