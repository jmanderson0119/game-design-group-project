using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// deployed audio and visual cues of the player using the shield ability + changes player behavior while ability is active
public class PlayerShield : MonoBehaviour
{

    // Tmarkers or time markers: used to handle cooldown behavior
    private float shieldTBuffer;
    private float shieldTMarker;
    
    bool firstShield = true; // used to negate cooldown effects at the beginning of the level
    private Animator animator; // reference to the player animation state machine

    // references used to deploy the audio cue for the shield activation
    private AudioSource shieldSource;
    [SerializeField] private AudioClip shieldNoise;



    // Start is called before the first frame update
    void Start()
    {
        // initialization of important player data
        shieldTBuffer = gameObject.GetComponent<PlayerStats>().ShieldTBuffer();
        animator = GetComponent<Animator>();
        shieldSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if shield is attempting to be deployed and if cooldown timer has been met
        if (Input.GetKeyDown(KeyCode.I) && ((Time.time >= shieldTMarker + shieldTBuffer) || (firstShield)) && gameObject.GetComponent<PlayerStats>().CanShield())
        {
            shieldSource.PlayOneShot(shieldNoise, 1f); // audio cue!
            animator.SetInteger("shielding", 1); // visual cue!
            if (firstShield) { firstShield = false; } // cooldown doesn't apply if player just start fighting stuff!
            shieldTMarker = Time.time; // mark the time!
            StartCoroutine(ShieldTimer()); // start the timer!
        }
    }

    // sets important player behavior: player cannot use other abilities or attack, but does get a speed boost
    IEnumerator ShieldTimer()
    {
        gameObject.GetComponent<PlayerStats>().Dash(false);
        gameObject.GetComponent<PlayerStats>().Shoot(false);
        gameObject.GetComponent<PlayerStats>().Melee(false);
        gameObject.GetComponent<PlayerStats>().DamageState(false);
        gameObject.GetComponent<PlayerMovement>().IncSpeed(0.75f);
        yield return new WaitForSeconds(4);
        animator.SetInteger("shielding", 0); // take down the shield visual
        gameObject.GetComponent<PlayerMovement>().IncSpeed(-0.75f);
        gameObject.GetComponent<PlayerStats>().DamageState(true);
        gameObject.GetComponent<PlayerStats>().Dash(true);
        gameObject.GetComponent<PlayerStats>().Shoot(true);
        gameObject.GetComponent<PlayerStats>().Melee(true);
    }
}
