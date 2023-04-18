using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    PlayerStats PlayerStats;
    public void UpgradeHealth() {
        PlayerStats.IncHealth(5);

    }

    public void UpgradeMelee()
    {
        PlayerStats.IncMeleeDamage(2);
        
    }

    public void UpgradeRange() {
        PlayerStats.IncRangedDamage(1);
        PlayerStats.IncBulletSpeed(2);
    }
}
