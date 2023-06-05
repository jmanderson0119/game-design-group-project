using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this script will display the cooldown GUI when the dash ability is deployed
public class DashCooldown : MonoBehaviour
{
    private float currentTime = 0f; // tracks the current time
    private float startingTime; // tracks the starting time
    [SerializeField] TMP_Text dashCooldown; // dash cooldown UI
    [SerializeField] bool dashCooldownActive = false;
    private float dashTMarker; // marks when the ability was deployed
    private float dashTBuffer; // ability cooldown timer
    private bool firstDash; // used for negating cooldown effects right when the player enters the level

    // Start is called before the first frame update
    void Start()
    {
        // initialization of important data
        dashCooldown.text = "";
        startingTime = GameObject.Find("mainPlayer").GetComponent<PlayerStats>().DashTBuffer();
        currentTime = startingTime;
        dashTBuffer = GameObject.Find("mainPlayer").GetComponent<PlayerStats>().DashTBuffer();

    }

    // Update is called once per frame
    void Update()
    {
        // ensures cooldown has been met and makes sure coodown is not in place when level begins
        if (GameObject.Find("mainPlayer").GetComponent<PlayerStats>().CanDash() && Input.GetKeyDown(KeyCode.J) && ((Time.time >= dashTMarker + dashTBuffer) || (firstDash)))
        {
            if (firstDash) { firstDash = false; }
            dashTMarker = Time.time;
            StartCoroutine(DashCooldownTimer());
        }
        
        //begins the cooldown
        if (dashCooldownActive)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime >= 0)
            {
                dashCooldown.text = "dash: " + currentTime.ToString("0") + "s";
            } 
            else 
            { 
                dashCooldown.text = ""; 
            }
        }
        
    }

    //keeps dashCooldownActive set to false until cooldown has been met + sets display so long a cooldown is up
    IEnumerator DashCooldownTimer()
    {
        dashCooldownActive = true;
        dashCooldown.text = "dash: 3s";
        yield return new WaitForSeconds(3.0f);
        dashCooldownActive = false;
        dashCooldown.text = "";
        currentTime = 3f;
    }
}
