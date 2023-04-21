using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoughtSkill : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;
    
    public TMP_Text skilldescription;
    public string skillviewed;

    void Start() {
        player = GameObject.Find("mainPlayer");
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    public void getDescription() {
        skillviewed = skilldescription.text;
        Debug.Log(skillviewed);
        if (skillviewed.Equals("Gain the ability to quickly manuever in the direction you're facing with the 'K' key. Costs : X Money, Y Reputation"))
        {
            playerStats.Dash(true);
        }
        else if (skillviewed.Equals("Gain the ability to attack from a safe distance with the 'O' key. Costs : X Money, Y Reputation ")) 
        {
            playerStats.Shoot(true);
        }
        else if (skillviewed.Equals("Gain the ability to defend againts enemy attacks with the 'I' key. Costs: X Money, Y Reputation"))
        {
            playerStats.Shield(true);
        }
    }
}
