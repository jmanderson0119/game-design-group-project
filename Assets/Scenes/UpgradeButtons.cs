using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public PlayerStats player;
    public void UpgradeHealth() {
        player.IncHealth(5);

    }

    public void UpgradeMelee()
    {
        player.IncMeleeDamage(2);
        
    }

    public void UpgradeRange() {
        player.IncRangedDamage(1);
        player.IncBulletSpeed(2);
    }
}
