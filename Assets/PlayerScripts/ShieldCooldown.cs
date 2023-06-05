using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this script handles the cooldown for the shield
public class ShieldCooldown : MonoBehaviour
{
    // cooldown tracking + activation data
    private float currentTime = 0f;
    private float startingTime;
    [SerializeField] TMP_Text shieldCooldown;
    [SerializeField] bool shieldCooldownActive = false;
    private float shieldTMarker;
    private float shieldTBuffer;
    bool firstShield = true;

    // Start is called before the first frame update: set text for cooldown GUI + initialization of time marking data
    void Start()
    {
        shieldCooldown.text = "";
        startingTime = GameObject.Find("mainPlayer").GetComponent<PlayerStats>().ShieldTBuffer();
        currentTime = startingTime;
        shieldTBuffer = GameObject.Find("mainPlayer").GetComponent<PlayerStats>().ShieldTBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        // so long as the player can shield and wishes to deploy the ability (and cooldown has been met obviously)
        if (GameObject.Find("mainPlayer").GetComponent<PlayerStats>().CanShield() && Input.GetKeyDown(KeyCode.I) && ((Time.time >= shieldTMarker + shieldTBuffer) || (firstShield)))
        {
            // will not apply cooldown at the beginning of the level
            if (firstShield) { firstShield = false; }
            shieldTMarker = Time.time; // mark the time!
            StartCoroutine(ShieldCooldownTimer()); // set the timer!
        }
        
        // track the time left and display it next to the player
        if (shieldCooldownActive)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime > 0)
            {
                shieldCooldown.text = "shield: " + currentTime.ToString("0") + "s";
            }
            else
            {
                shieldCooldown.text = "";
            }
        }
    }

    // waits until cooldown is over the empty the cooldown text and deactivate the cooldown
    IEnumerator ShieldCooldownTimer()
    {
        shieldCooldownActive = true;
        shieldCooldown.text = "shield: 6s";
        yield return new WaitForSeconds(6.0f);
        shieldCooldownActive = false;
        shieldCooldown.text = "";
        currentTime = 6f;   
    }
}
