using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    //[SerializeField] private GameObject shieldPrefab; outdated shield indicator
    private GameObject shield;

    private float shieldTBuffer;
    private float shieldTMarker;

    bool firstShield = true;
    private Animator animator;

    private AudioSource shieldSource;
    [SerializeField] private AudioClip shieldNoise;



    // Start is called before the first frame update
    void Start()
    {
        shieldTBuffer = gameObject.GetComponent<PlayerStats>().ShieldTBuffer();
        animator = GetComponent<Animator>();
        shieldSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && ((Time.time >= shieldTMarker + shieldTBuffer) || (firstShield)) && gameObject.GetComponent<PlayerStats>().CanShield())
        {
            shieldSource.PlayOneShot(shieldNoise, 1f);
            animator.SetInteger("shielding", 1);
            if (firstShield) { firstShield = false; }
            shieldTMarker = Time.time;
            StartCoroutine(ShieldTimer());
        }
    }

    IEnumerator ShieldTimer()
    {
        gameObject.GetComponent<PlayerStats>().Dash(false);
        gameObject.GetComponent<PlayerStats>().Shoot(false);
        gameObject.GetComponent<PlayerStats>().Melee(false);
        gameObject.GetComponent<PlayerStats>().DamageState(false);
        gameObject.GetComponent<PlayerMovement>().IncSpeed(0.75f);
        yield return new WaitForSeconds(4);
        animator.SetInteger("shielding", 0);
        gameObject.GetComponent<PlayerMovement>().IncSpeed(-0.75f);
        gameObject.GetComponent<PlayerStats>().DamageState(true);
        gameObject.GetComponent<PlayerStats>().Dash(true);
        gameObject.GetComponent<PlayerStats>().Shoot(true);
        gameObject.GetComponent<PlayerStats>().Melee(true);
        Destroy(shield);
    }
}
