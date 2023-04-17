using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // player stats that can be adjusted in hte shop
    [SerializeField] private float health = 100.0f;
    [SerializeField] private int meleeDamage = 2;
    [SerializeField] private int rangedDamage = 3;
    [SerializeField] private float reputation = 0.0f;
    [SerializeField] private int gold = 0;
    [SerializeField] private float meleeTBuffer = 0.5f;
    [SerializeField] private float bulletTBuffer = 0.5f;
    [SerializeField] private float dashTBuffer = 1f;
    [SerializeField] private float dashSpeed = 16f;
    [SerializeField] private float dashLength = 0.14f;
    [SerializeField] private float speed = 3.2f;
    [SerializeField] private float bulletSpeed = 7.0f;

    //booleans used across player ability scripts
    [SerializeField] private bool canMelee = true;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private Vector3 dashDirection;
    [SerializeField] private bool canShield = true;

    private float dashTMarker;

    // getters for player stats
    public bool CanMelee() => canMelee;
    public bool CanShoot() => canShoot;
    public bool CanDash() => canDash;
    public bool CanShield() => canShield;

    public float Health() => health;
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
    public void IncBulletTBuffer(float increment) { bulletTBuffer += increment; }
    public void IncMeleeTBuffer(float increment) { meleeTBuffer += increment; }
    public void IncReputation(float increment) { reputation += increment; }
    public void IncRangedDamage(int increment) { rangedDamage += increment; }
    public void IncMeleeDamage(int increment) { meleeDamage += increment; }
    public void IncHealth(float increment) { health += increment; }
    public void IncBulletSpeed(float increment) { bulletSpeed += increment; }
    public void IncGold(int increment) { gold += increment; }


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        // start dashing if J is pressed
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= dashTMarker + dashTBuffer)
        {
            dashTMarker = Time.time;

            float horizontalDash = Input.GetAxis("Horizontal") * Time.deltaTime;
            float verticalDash = Input.GetAxis("Vertical") * Time.deltaTime;
            dashDirection = new Vector3(horizontalDash, verticalDash, 0).normalized;

            StartCoroutine(DashTimer());
        }
    }

    // Tracks how long the player's dash should last
    IEnumerator DashTimer()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
    }
}