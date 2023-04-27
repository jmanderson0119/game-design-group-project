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
        
        if (skillviewed.Equals("Gain the ability to quickly manuever in the direction you're facing with the 'J' key. Costs : 500 Money, 999 Reputation"))
        {
            if (playerStats.CanDash() == false)
            {
                playerStats.IncGold(-500);
                playerStats.IncReputation(-999);
                if (playerStats.Gold() >= 0 && playerStats.Reputation() >= 0)
                {
                    Debug.Log("The player has lost 500 Cash");
                    Debug.Log("The player has lost 999 Reputation");
                    playerStats.Dash(true);
                    Debug.Log("The player can Dash : " + playerStats.CanDash());
                }
                else
                {
                    playerStats.IncGold(500);
                    playerStats.IncReputation(999);
                    Debug.Log("The player did not have enough Cash or Reputation to buy this skill");
                    playerStats.Dash(false);
                }
            }
            else {
                skilldescription.text = "You already own this skill....";
            }
            
        }
        else if (skillviewed.Equals("Gain the ability to attack from a safe distance with the 'O' key. Costs : 75 Money, 150 Reputation")) 
        {
            if (playerStats.CanShoot() == false) {
                playerStats.IncGold(-75);
                playerStats.IncReputation(-150);
                if (playerStats.Gold() >= 0 && playerStats.Reputation() >= 0)
                {
                    Debug.Log("The player has lost 75 Cash");
                    Debug.Log("The player has lost 150 Reputation");
                    playerStats.Shoot(true);
                    Debug.Log("The player can Shoot : " + playerStats.CanShoot());
                }
                else
                {
                    playerStats.IncGold(75);
                    playerStats.IncReputation(150);
                    Debug.Log("The player did not have enough Cash or Reputation to buy this skill");
                    playerStats.Shoot(false);
                }
            }
            else
            {
                skilldescription.text = "You already own this skill....";
            }
            
        }
        else if (skillviewed.Equals("Gain the ability to defend againts enemy attacks with the 'I' key. Costs 250 Money, 600 Reputation"))
        {
            if(playerStats.CanShield() == false)
            {
                playerStats.IncGold(-250);
                playerStats.IncReputation(-600);
                if (playerStats.Gold() >= 0 && playerStats.Reputation() >= 0)
                {
                    Debug.Log("The player has lost 250 Cash");
                    Debug.Log("The player has lost 600 Reputation");
                    playerStats.Shield(true);
                    Debug.Log("The player can Shield : " + playerStats.CanShield());
                }
                else
                {
                    playerStats.IncGold(250);
                    playerStats.IncReputation(600);
                    Debug.Log("The player did not have enough Cash or Reputation to buy this skill");
                    playerStats.Shield(false);
                }
            }
            else
            {
                skilldescription.text = "You already own this skill....";
            }
           
        }
    }
}
