using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class IdentityHandler : Handler
{
    [Header("Referencing")]
    public List<GameObject> playerObjects;
    public GameObject dummyDeadBody;
    [Header("Stats")]
    public float ChangeIdentityCooldown;
    public List<GameObject> deadPlayerObjects;
    private int currentCharacterIndex;
    private float CurrentChangeIdentityCooldown;
    void Start()
    {
        CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
    }
    void Update()
    {
        CurrentChangeIdentityCooldown -= Time.deltaTime;
        if (CurrentChangeIdentityCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchIdentity(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchIdentity(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchIdentity(2);
            CurrentChangeIdentityCooldown = ChangeIdentityCooldown;
        }
    }

    public void SwitchIdentity(int identity)
    {
        if (playerObjects[identity] == playerObjects[currentCharacterIndex]) return;
        if (!IsIdentityAlive(playerObjects[identity])) return;
        playerObjects[identity].transform.position = playerObjects[currentCharacterIndex].transform.position;
        playerObjects[currentCharacterIndex].SetActive(false);
        playerObjects[identity].SetActive(true);
        RetrackVCam(playerObjects[identity]);
        currentCharacterIndex = identity;
    }
    public void RemoveIdentity()
    {
        Debug.Log(playerObjects[currentCharacterIndex].name);
        deadPlayerObjects.Add(playerObjects[currentCharacterIndex]);
        ThrowOutDeadIdentity(playerObjects[currentCharacterIndex]);
        if (currentCharacterIndex >= 2) SwitchIdentity(currentCharacterIndex - 1);
        else SwitchIdentity(currentCharacterIndex + 1);
    }
    public void ThrowOutDeadIdentity(GameObject deadPlayer)
    {
        dummyDeadBody.GetComponent<SpriteRenderer>().sprite = deadPlayer.GetComponent<SpriteRenderer>().sprite;
        dummyDeadBody.transform.localScale = deadPlayer.transform.localScale;
        Instantiate(dummyDeadBody, deadPlayer.transform.position, deadPlayer.transform.rotation);
    }
    public bool IsIdentityAlive(GameObject playerObject)
    {
        foreach (GameObject player in deadPlayerObjects)
        {
            if (player == playerObject) return false;
        }
        return true;
    }
    private void RetrackVCam(GameObject newIdentity)
    {
        GameObject.FindGameObjectWithTag("VCam").GetComponent<CinemachineVirtualCamera>().Follow = newIdentity.transform;
    }
    public void ReviveDeadPlayer()
    {
        deadPlayerObjects.Clear();
    }
}
