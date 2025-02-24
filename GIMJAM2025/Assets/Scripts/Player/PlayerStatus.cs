using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int playerSlot;
    public bool isDead = false;
    public float reviveCooldown;
    private Player[] playerScripts;
    public GameObject[] playerPrefab;

    void Start()
    {
        playerScripts = GetComponents<Player>();
    }
    void Update()
    {
        if(isDead){
            foreach(Player player in playerScripts){
                player.enabled = false;
            }
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(StartRevive(playerSlot));
        }
    }

    IEnumerator StartRevive(int index)
    {
        yield return new WaitForSeconds(reviveCooldown);
        GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
        for(int i=0;i<allPlayer.Length;i++){
            if(allPlayer[i].GetComponent<PlayerStatus>().isDead == false){
                allPlayer[i].GetComponent<ManipulateIdentity>().playerObjects.RemoveAt(index);
                allPlayer[i].GetComponent<ManipulateIdentity>().playerObjects.Insert(index, playerPrefab[playerSlot]);
                Debug.Log("Revived");
            }
        }
        foreach(Player player in playerScripts){
            player.enabled = true;
        }
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        isDead = false;
        Destroy(gameObject);
    }
}
