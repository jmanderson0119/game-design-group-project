using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;
    public int healthcap = 0;
    public int meleecap = 0;
    public int rangecap = 0;
    
    void Start()
    {
        player = GameObject.Find("mainPlayer");
        playerStats = player.GetComponent<PlayerStats>();
        Debug.Log("The player's Health: " + playerStats.MaxHealth());
        Debug.Log("The player's Melee DMG: " + playerStats.MeleeDamage());
        Debug.Log("The player's Ranged DMG: " + playerStats.RangedDamage());
        Debug.Log("The player's Ranged Speed: " + playerStats.BulletSpeed());
    }
    public void UpgradeHealth() {
        playerStats.IncGold(-250);
        if (playerStats.Gold() >= 0)
        {
            if (healthcap <= 10)
            {
                playerStats.IncMaxHealth(2);
                Debug.Log("The player's Health: " + playerStats.MaxHealth());
                healthcap = healthcap + 1;
            }
            else
            {
                Debug.Log("Player is at MAX HEALTH.");
            }
        }
        else
        {
            playerStats.IncGold(250);
            Debug.Log("Player doesn't have enough money to buy this upgrade");
        }
    }

    public void UpgradeMelee()
    {
        playerStats.IncGold(-500);
        if (playerStats.Gold() >= 0)
        {
            if (meleecap <= 10)
            {
                playerStats.IncMeleeDamage(3);
                Debug.Log("The player's Melee DMG: " + playerStats.MeleeDamage());
                meleecap = meleecap + 1;
            }
            else
            {
                Debug.Log("Player is at MAX MELEE.");
            }
        }
        else {
            playerStats.IncGold(500);
            Debug.Log("Player doesn't have enough money to buy this upgrade");
        }
        
    }

    public void UpgradeRange() {
        playerStats.IncGold(-1000);
        if (playerStats.Gold() >= 0) {
            if(rangecap <= 10)
            {
                playerStats.IncRangedDamage(5);
                playerStats.IncBulletSpeed(0.2f);
                Debug.Log("The player's Ranged DMG: " + playerStats.RangedDamage());
                Debug.Log("The player's Ranged Speed: " + playerStats.BulletSpeed());
                rangecap = rangecap + 1;
            }
            else
            {
                Debug.Log("Player is at MAX RANGE.");
            }
        }
        else
        {
            playerStats.IncGold(1000);
            Debug.Log("Player doesn't have enough money to buy this upgrade");
        }
        
        
    }
}
