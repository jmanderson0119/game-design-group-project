using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashCooldown : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime;
    [SerializeField] TMP_Text dashCooldown; // dash cooldown UI
    [SerializeField] bool dashCooldownActive = false;
    private float dashTMarker;
    private float dashTBuffer;
    private bool firstDash;

    // Start is called before the first frame update
    void Start()
    {
        dashCooldown.text = "";
        startingTime = GameObject.Find("mainPlayer").GetComponent<PlayerStats>().DashTBuffer();
        currentTime = startingTime;
        dashTBuffer = GameObject.Find("mainPlayer").GetComponent<PlayerStats>().DashTBuffer();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("mainPlayer").GetComponent<PlayerStats>().CanDash() && Input.GetKeyDown(KeyCode.J) && ((Time.time >= dashTMarker + dashTBuffer) || (firstDash)))
        {
            if (firstDash) { firstDash = false; }
            dashTMarker = Time.time;
            StartCoroutine(DashCooldownTimer());
        }

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
