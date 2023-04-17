using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls player WASD movement and tracks the direction the player is facing at all times
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // figure for player movement speed 
    [SerializeField] private Vector3 direction = new Vector3(1f, 0f, 0f); // last direction the player moved in as a 2D vector with magnitude 1

    //private CharacterController characterController; // player's Character Controller
    private GameObject player; // used to obtain player
    private PlayerStats playerStats; // used to obtain player statistics
    public Animator animator; // player animator
    private Rigidbody2D _Rigidbody; // player rigid body

    // returns player's direction (see RangedAttack.cs)
    public Vector3 getDirection() => direction;

    // Start is called before the first frame update; obtain character controller for player movement
    void Start()
    {
        //characterController = GetComponent<CharacterController>(); // obtain character controller for updating movement
        _Rigidbody = GetComponent<Rigidbody2D>();

        //player = GameObject.Find("mainPlayer"); // obtain player
        playerStats = GetComponent<PlayerStats>(); // obtain player statistics
        speed = playerStats.Speed(); // obtain player's speed and store in speed

    }

    // Update is called once per frame; used for player WASD movement
    void Update()
    {

        if (!playerStats.Dashing())
        {
            float horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime; // figure for horizontal player direction
            float verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime; // figure for vertical player direction

            Vector3 playerMovement = (new Vector3(horizontalMovement, verticalMovement, 0)).normalized; // vector for direction player is moving in

            if (playerMovement.magnitude > 0) // gives the direction the player is facing in and stores in direction (for use in BulletTravel)
            {
                direction = playerMovement; // direction is now a vector of magnitude 1
            }

            _Rigidbody.velocity = playerMovement * speed;
        }
    }
}

