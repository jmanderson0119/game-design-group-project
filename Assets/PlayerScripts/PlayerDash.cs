using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script works in tandem with PlayerStats.cs to handle the dashing ability behavior. see PlayerStats.cs for more context
public class PlayerDash : MonoBehaviour
{
    private GameObject player; // used to obtain player
    private PlayerStats playerStats; // used to obtain player statistic script
    private Rigidbody2D _rigidBody; // the player's rigid body

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("mainPlayer"); // obtain player object
        playerStats = player.GetComponent<PlayerStats>(); // obtain player stats script
        _rigidBody = player.GetComponent<Rigidbody2D>(); // obtain the player's rigid body
    }

    // Update is called once per frame
    void Update()
    {
        // use this movement behavior if the player is still in the process of dashing
        if (playerStats.Dashing())
        {
            _rigidBody.velocity = playerStats.DashDirection() * playerStats.DashSpeed();
        }
    }
}
