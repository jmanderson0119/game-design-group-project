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
        Debug.Log("The player can Shield : " + playerStats.CanShield());
        Debug.Log("The player can Shoot : " + playerStats.CanShoot());
        Debug.Log("The player can Dash : " + playerStats.CanDash());
    }

    // Start is called before the first frame update
    public void getDescription() {
        skillviewed = skilldescription.text;
        Debug.Log(skillviewed);
        
        if (skillviewed.Equals("Gain the ability to quickly manuever in the direction you're facing with the 'J' key. Costs : X Money, Y Reputation"))
        {
            playerStats.Dash(true);
            Debug.Log("The player can Dash : " + playerStats.CanDash());
        }
        else if (skillviewed.Equals("Gain the ability to attack from a safe distance with the 'O' key. Costs : X Money, Y Reputation ")) 
        {
            playerStats.Shoot(true);
            Debug.Log("The player can Shoot : " + playerStats.CanShoot());
        }
        else if (skillviewed.Equals("Gain the ability to defend againts enemy attacks with the 'I' key. Costs: X Money, Y Reputation"))
        {
            playerStats.Shield(true);
            Debug.Log("The player can Shield : " + playerStats.CanShield());
        }
    }
}
