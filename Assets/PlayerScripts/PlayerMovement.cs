using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls player WASD movement and tracks the direction the player is facing at all times
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // figure for player movement speed 
    [SerializeField] private Vector3 direction = new Vector3(1f, 0f, 0f); // last direction the player moved in as a 2D vector with magnitude 

    //private CharacterController characterController; // player's Character Controller
    private GameObject player; // used to obtain player
    private PlayerStats playerStats; // used to obtain player statistics
    private Animator animator; // player animator
    private Rigidbody2D _Rigidbody; // player rigid body

    // returns player's direction (see RangedAttack.cs)
    public Vector3 getDirection() => direction;
    public void IncSpeed(float increment) { speed += increment; }

    // Start is called before the first frame update; obtain character controller for player movement
    void Start()
    {
        //characterController = GetComponent<CharacterController>(); // obtain character controller for updating movement
        _Rigidbody = GetComponent<Rigidbody2D>();

        //player = GameObject.Find("mainPlayer"); // obtain player
        playerStats = GetComponent<PlayerStats>(); // obtain player statistics
        speed = playerStats.Speed(); // obtain player's speed and store in speed
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame; used for player WASD movement
    void Update()
    {

        if (!playerStats.Dashing())
        {
            float horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime; // figure for horizontal player direction
            float verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime; // figure for vertical player direction

            float playerMag = new Vector3(horizontalMovement, verticalMovement).magnitude;
            //float playerXMag = playerMag.x;
            Vector3 playerMovement = (new Vector3(horizontalMovement, verticalMovement, 0)).normalized; // vector for direction player is moving in

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && playerMag == 0)
            {
                animator.SetInteger("walkingMode", 0);
                animator.SetInteger("MeleeDirection", 4);
            }
            else if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)) || (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)))
            {
                animator.SetInteger("walkingMode", 1);
                animator.SetInteger("MeleeDirection", 4);
            }
            else if ((!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) || (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)) || (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) || (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)))
            {
                animator.SetInteger("walkingMode", 2);
                animator.SetInteger("MeleeDirection", 3);
            }
            else if ((Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)))
            {
                animator.SetInteger("walkingMode", 3);
                animator.SetInteger("MeleeDirection", 1);
            }
            else if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) || (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)))
            {
                animator.SetInteger("walkingMode", 4);
                animator.SetInteger("MeleeDirection", 2);
            }

                if (playerMovement.magnitude > 0) // gives the direction the player is facing in and stores in direction (for use in BulletTravel)
                {
                    direction = playerMovement; // direction is now a vector of magnitude 1
                }

                _Rigidbody.velocity = playerMovement * speed;
            }
    }
}

