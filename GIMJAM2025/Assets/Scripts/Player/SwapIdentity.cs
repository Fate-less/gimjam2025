using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwapIdentity : Player
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
                ChangeIdentity(1);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeIdentity(2);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeIdentity(3);
            }
        }
    }

    void ChangeIdentity(int identity){
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
}
