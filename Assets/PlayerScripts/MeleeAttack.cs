using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles melee attacks on all interactable objects 
public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private float meleeTBuffer; // delay between player's melee attacks
    [SerializeField] private int meleeDmg; // player's melee attack damage
    [SerializeField] GameObject meleeVisual; // visual indicator of what the melee attack actually affects
    private AudioSource meleeSource;
    [SerializeField] private AudioClip meleeNoise;


    private float meleeTMarker; // time that the most recent melee attack occurred
    //private GameObject meleeAoe; // visual indicator instance
    private GameObject player; // used to obtain player
    private PlayerStats playerStats; // used to obtain player statistic script
    private Animator animator;
    private PlayerMovement playerMovement;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("mainPlayer"); // obtain player
        playerStats = player.GetComponent<PlayerStats>(); // obtain player stats
        playerMovement = player.GetComponent<PlayerMovement>();
        meleeDmg = playerStats.MeleeDamage(); // get the player melee attack damage and save in meleeDmg
        meleeTBuffer = playerStats.MeleeTBuffer(); // get the player melee attack delay and save in meleeTBuffer
        animator = GetComponent<Animator>();
        meleeSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame; used to track and handle anything within range of a melee attack
    void Update()
    {
        // melee logic runs if spacebar is pressed after the attack delay
        if (playerStats.CanMelee() && Input.GetKeyDown(KeyCode.K) && Time.time >= meleeTMarker + meleeTBuffer)
        {
            animator.SetInteger("stabbing", 1); // provides updates for the animation state machine
            StartCoroutine(MeleeStabbingReset()); // reset the state machine info once stab is completed
            meleeTMarker = Time.time; // update time of last melee attack
            meleeSource.PlayOneShot(meleeNoise, 0.75f); // audio cue for player melee
            
            /* 
            melee behavior must be handled differently based on which direction the player is facing:
            the general guideline is determine which way the hitbox should be oriented based on which direction
            the player is facing, then determine if an enemy was hit and handle the sfx and enemy stats accordingly.
            */
            switch (animator.GetInteger("MeleeDirection"))
            {               
                case 1:
                    hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.54f), Vector2.up, 0.82f);
                    if (hit.collider != null && hit.collider.transform.parent == null)
                    {
                        Debug.Log("Enemy was hit");
                        EnemyBehavior behavior = hit.collider.GetComponent<EnemyBehavior>();
                        behavior.health -= meleeDmg;
                        behavior.damage();
                        behavior.gameObject.transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);
                    }
                    break;
                case 2:
                    hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.54f), Vector2.down, 0.82f);
                    if (hit.collider != null && hit.collider.transform.parent == null)
                    {
                        Debug.Log("Enemy was hit");
                        EnemyBehavior behavior = hit.collider.GetComponent<EnemyBehavior>();
                        behavior.health -= meleeDmg;
                        behavior.damage();
                        behavior.gameObject.transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);
                    }
                    break;
                case 3:
                    hit = Physics2D.Raycast(new Vector2(transform.position.x - 0.42f, transform.position.y), Vector2.left, 0.82f);
                    if (hit.collider != null && hit.collider.transform.parent == null)
                    {
                        Debug.Log("Enemy was hit");
                        EnemyBehavior behavior = hit.collider.GetComponent<EnemyBehavior>();
                        behavior.health -= meleeDmg;
                        behavior.damage();
                        behavior.gameObject.transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);
                    }
                    break;
                case 4:
                    hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.42f, transform.position.y), Vector2.right, 0.82f);
                    if (hit.collider != null && hit.collider.transform.parent == null)
                    {
                        Debug.Log("Enemy was hit");
                        EnemyBehavior behavior = hit.collider.GetComponent<EnemyBehavior>();
                        behavior.health -= meleeDmg;
                        behavior.damage();
                        behavior.gameObject.transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);
                    }
                    break;
            }
        }
    }
    
    // resets the stabbing animation after enough time has passed
    IEnumerator MeleeStabbingReset()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetInteger("stabbing", 0);
    }
    
}
