using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles melee attacks on all interactable objects 
public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private float meleeTBuffer; // delay between player's melee attacks
    [SerializeField] private int meleeDmg; // player's melee attack damage
    [SerializeField] GameObject meleeVisual; // visual indicator of what the melee attack actually affects


    private float meleeTMarker; // time that the most recent melee attack occurred
    //private GameObject meleeAoe; // visual indicator instance
    private GameObject player; // used to obtain player
    private PlayerStats playerStats; // used to obtain player statistic script
    private Animator animator;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("mainPlayer"); // obtain player
        playerStats = player.GetComponent<PlayerStats>(); // obtain player stats
        playerMovement = player.GetComponent<PlayerMovement>();
        meleeDmg = playerStats.MeleeDamage(); // get the player melee attack damage and save in meleeDmg
        meleeTBuffer = playerStats.MeleeTBuffer(); // get the player melee attack delay and save in meleeTBuffer
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame; used to track and handle anything within range of a melee attack
    void Update()
    {

        if (playerStats.CanMelee() && Input.GetKeyDown(KeyCode.K) && Time.time >= meleeTMarker + meleeTBuffer) // melee logic runs if spacebar is pressed after the attack delay
        {
            animator.SetInteger("stabbing", 1);
            StartCoroutine(MeleeStabbingReset());
            meleeTMarker = Time.time; // update time of last melee attack

            Collider2D[] meleeTargets = Physics2D.OverlapCircleAll(transform.position, 1.28f); // list of all things hit by the melee

            //GameObject meleeAoe = Instantiate(meleeVisual) as GameObject; // visual indicator becomes visual
            //eleeAoe.transform.position = transform.position; // visual indicator laid over player
            //meleeAoe.transform.parent = this.transform; // visual indicator will follow the player
            //Destroy(meleeAoe, 0.22f); // destroy visual indicator

            // event handler for damageable/interactable targets
            foreach (Collider2D target in meleeTargets)
            {
                if (target.transform.parent == null)
                {
                    EnemyBehavior behavior = target.gameObject.GetComponent<EnemyBehavior>();

                    behavior.health -= meleeDmg;
                    target.gameObject.transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);
                    Debug.Log("Enemy Health: " + behavior.health);
                }
            }
        }
    }
    
    IEnumerator MeleeStabbingReset()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetInteger("stabbing", 0);
    }
    
}