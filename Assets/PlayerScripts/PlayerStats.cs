using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private static float maxHealth = 10.0f;
    [SerializeField] private static float health = 10.0f;
    [SerializeField] private static int meleeDamage = 2;
    [SerializeField] private static int rangedDamage = 3;
    [SerializeField] private static float reputation = 0.0f;
    [SerializeField] private static int gold = 0;
    [SerializeField] private static float meleeTBuffer = 0.5f;
    [SerializeField] private static float bulletTBuffer = 0.5f;
    [SerializeField] private static float dashTBuffer = 3f;
    [SerializeField] private static float dashSpeed = 16f;
    [SerializeField] private static float dashLength = 0.14f;
    [SerializeField] private static float speed = 3.2f;
    [SerializeField] private static float bulletSpeed = 9.0f;
    [SerializeField] private static float shieldTBuffer = 6f;
    public static int BountiesCompleted = 0;
    

    //booleans used across player ability scripts
    [SerializeField] private static bool canMelee = true;
    [SerializeField] private static bool canShoot = true;
    [SerializeField] private static bool canDash = true;
    [SerializeField] private static bool canShield = true;
    [SerializeField] private static bool damageable = true;
    [SerializeField] private static bool isDashing = false;
    [SerializeField] private static Vector3 dashDirection;

    private float dashTMarker;
    private Animator animator;
    private AudioSource dashSource;
    [SerializeField] private AudioClip dashNoise;

    // getters for player stats
    public bool Damageable() => damageable;
    public bool CanMelee() => canMelee;
    public bool CanShoot() => canShoot;
    public bool CanDash() => canDash;
    public bool CanShield() => canShield;

    public float MaxHealth() => maxHealth;
    public float Health() => health;
    public float ShieldTBuffer() => shieldTBuffer;
    public float DashTBuffer() => dashTBuffer;
    public Vector3 DashDirection() => dashDirection;
    public bool Dashing() => isDashing;
    public float DashLength() => dashLength;
    public float DashSpeed() => dashSpeed;
    public int MeleeDamage() => meleeDamage;
    public int RangedDamage() => rangedDamage;
    public float Reputation() => reputation;
    public float Gold() => gold;
    public float Speed() => speed;
    public float BulletSpeed() => bulletSpeed;
    public float MeleeTBuffer() => meleeTBuffer;
    public float BulletTBuffer() => bulletTBuffer;

    // change any of the following values by floating value increment
    public void IncSpeed(float increment) { speed += increment; }
    public void IncMaxHealth(float increment) { maxHealth += increment; }
    public void IncBulletTBuffer(float increment) { bulletTBuffer += increment; }
    public void IncMeleeTBuffer(float increment) { meleeTBuffer += increment; }
    public void IncReputation(float increment) { reputation += increment; }
    public void IncRangedDamage(int increment) { rangedDamage += increment; }
    public void IncMeleeDamage(int increment) { meleeDamage += increment; }
    public void IncHealth(float increment) { health += increment; }
    public void IncBulletSpeed(float increment) { bulletSpeed += increment; }
    public void IncGold(int increment) { gold += increment; }
    public void Melee(bool melee) { canMelee = melee; }
    public void Shoot(bool shoot) { canShoot = shoot; }
    public void Dash(bool dash) { canDash = dash; }
    public void Shield(bool shield) { canShield = shield; }
    public void DamageState(bool damageState) { damageable = damageState; }



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dashSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // start dashing if J is pressed
        if (CanDash() && Input.GetKeyDown(KeyCode.J) && ((Time.time >= dashTMarker + dashTBuffer) || (Time.time < 3)))
        {
            dashSource.PlayOneShot(dashNoise, 0.75f);
            animator.SetInteger("dashing", 1);
            dashTMarker = Time.time;

            float horizontalDash = Input.GetAxis("Horizontal") * Time.deltaTime;
            float verticalDash = Input.GetAxis("Vertical") * Time.deltaTime;
            dashDirection = new Vector3(horizontalDash, verticalDash, 0).normalized;

            StartCoroutine(DashTimer());
        }
        if(health<=0){
            Die();
        }
    }

    // Tracks how long the player's dash should last
    IEnumerator DashTimer()
    {
        isDashing = true;
        Melee(false);
        Shoot(false);
        Shield(false);
        yield return new WaitForSeconds(dashLength);
        animator.SetInteger("dashing", 0);
        Melee(true);
        Shoot(true);
        Shield(true);
        isDashing = false;
    }
    public void Die(){
        if(GameObject.Find("EnemySpawners")==null){
            
        }
        else
        {
            GameObject.Find("EnemySpawners").GetComponent<MonsterCounter>().EarlyComplete();
        }
    }

    public void healToFull(){
        health = maxHealth;
    }
}