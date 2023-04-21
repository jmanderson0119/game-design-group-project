using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls the bullet travel event
public class BulletTravel : MonoBehaviour
{
    [SerializeField] private float bulletSpeed; // relative bullet speed figure
    [SerializeField] private float bulletDmg; // bullet damage

    private GameObject player; // player character (bullet needs direction of player)
    private PlayerStats playerStats; // used to obtain the player statistic script
    private PlayerMovement playerMovement; // the script stored here contains directional info

    private Vector3 travelDirection; // the directional vector3 for the bullet to travel in
    private Vector3 bulletMovement; // increment of the bullet position on update

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        EnemyBehavior behavior = collision.gameObject.GetComponent<EnemyBehavior>();

        if (collision.gameObject.transform.parent == null)
        {
            behavior.health -= (int) bulletDmg;
            behavior.damage();
            collision.gameObject.transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);
            Debug.Log("Enemy Health: " + behavior.health);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("mainPlayer"); // obtain player
        playerMovement = player.GetComponent<PlayerMovement>(); // obtain movement script
        Vector3 travelDirection = playerMovement.getDirection(); // get direction the player is facing

        playerStats = player.GetComponent<PlayerStats>(); // obtain player statistics
        bulletDmg = playerStats.RangedDamage(); // get player's ranged attack damage and store in bulletDmg
        bulletSpeed = playerStats.BulletSpeed(); // get the player's ranged attack speed and store in bulletSpeed

        this.transform.position = player.transform.position + travelDirection * 1.2f; // update the location of the bullet to be just in front of the player where they are facing
        bulletMovement = travelDirection * bulletSpeed; // stores movement increment relative to framerate, bulletSpeed, and the travelDirection
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += bulletMovement * Time.deltaTime; // update bullet position according to deltaTime, bulletSpeed, and travelDirection
    }
}