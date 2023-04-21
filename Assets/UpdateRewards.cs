using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateRewards : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;

    public TMP_Text SignPost;
    public string refresh;
    void Start()
    {
        player = GameObject.Find("mainPlayer");
        playerStats = player.GetComponent<PlayerStats>();
        Debug.Log("The player's Cash: " + playerStats.Gold());
        Debug.Log("The player's Reputation: " + playerStats.Reputation());
    }

    // Update is called once per frame
    void Update()
    {
        SignPost.text = "Player :\nCash : " + playerStats.Gold() +"\nReputation : " + playerStats.Reputation();
        
    }
}
