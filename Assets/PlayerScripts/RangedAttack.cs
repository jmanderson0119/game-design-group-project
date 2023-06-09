using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to instantiate the bullet
public class RangedAttack : MonoBehaviour
{
    // bullet instantiation
    [SerializeField] GameObject bulletPrefab; // the bullet GameObject to be instantiated
    [SerializeField] Transform bulletSpawn; // transform used for bullet instantiation

    // attack requirements
    [SerializeField] float bulletTBuffer; // ranged attack delay
    private float bulletTMarker; // time of most recent ranged attack

    // player stats to apply to attack
    private GameObject player; // used to obtain player
    private PlayerStats playerStats; // used to obtain player statistics

    private AudioSource bulletSource;
    [SerializeField] private AudioClip bulletNoise;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("mainPlayer"); // obtain player
        playerStats = player.GetComponent<PlayerStats>(); // obtain the player's stats
        bulletTBuffer = playerStats.BulletTBuffer(); // obtain the bullet time buffer and store in bulletTBuffer
        bulletSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.CanShoot() && Input.GetKeyDown(KeyCode.O) && Time.time > bulletTMarker + bulletTBuffer) // ranged attack instantiation once left click is pressed after the delay 
        {
            bulletTMarker = Time.time; // last ranged attack time marker is updated

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject; // bullet instantiation
            bulletSource.PlayOneShot(bulletNoise, 0.5f);
            Destroy(bullet, 0.75f); // destroy so that there are no invalid position input errors
        }
    }
}