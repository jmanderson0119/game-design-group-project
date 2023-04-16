using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // stats used across the four player scripts
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float meleeDamage = 10.0f;
    [SerializeField] private float rangedDamage = 8.0f;
    [SerializeField] private float meleeRange = 0.75f;
    [SerializeField] private float reputation = 0.0f;
    [SerializeField] private int gold = 0;
    [SerializeField] private float meleeTBuffer = 0.5f;
    [SerializeField] private float bulletTBuffer = 0.5f;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float bulletSpeed = 7.0f;


    // getters for player stats
    public float Health() => health;
    public float MeleeDamage() => meleeDamage;
    public float RangedDamage() => rangedDamage;
    public float MeleeRange() => meleeRange;
    public float Reputation() => reputation;
    public float Gold() => gold;
    public float Speed() => speed;
    public float BulletSpeed() => bulletSpeed;
    public float MeleeTBuffer() => meleeTBuffer;
    public float BulletTBuffer() => bulletTBuffer;

    // change any of the following values by floating value increment
    public void IncSpeed(float increment) { speed += increment; }
    public void IncBulletTBuffer(float increment) { bulletTBuffer += increment; }
    public void IncMeleeTBuffer(float increment) { meleeTBuffer += increment; }
    public void IncReputation(float increment) { reputation += increment; }
    public void IncRangedDamage(float increment) { rangedDamage += increment; }
    public void IncMeleeDamage(float increment) { meleeDamage += increment; }
    public void IncHealth(float increment) { health += increment; }
    public void IncMeleeRange(float increment) { meleeRange += increment; }
    public void IncBulletSpeed(float increment) { bulletSpeed += increment; }
    public void IncGold(int increment) { gold += increment; }


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        // quit the game when the player dies
        if (health <= 0.0f)
        {
            Die();
        }
    }
    void Die()
    {
        Application.Quit();
    }
}