using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public GameObject player;
    public PlayerStats playerStats;

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
        playerStats.IncMaxHealth(5);
        Debug.Log("The player's Health: " + playerStats.MaxHealth());
    }

    public void UpgradeMelee()
    {
        playerStats.IncMeleeDamage(2);
        Debug.Log("The player's Melee DMG: " + playerStats.MeleeDamage());
    }

    public void UpgradeRange() {
        playerStats.IncRangedDamage(1);
        playerStats.IncBulletSpeed(2);
        Debug.Log("The player's Ranged DMG: " + playerStats.RangedDamage());
        Debug.Log("The player's Ranged Speed: " + playerStats.BulletSpeed());
    }
}
