using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldCooldown : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime;
    [SerializeField] TMP_Text shieldCooldown;
    [SerializeField] bool shieldCooldownActive = false;
    private float shieldTMarker;
    private float shieldTBuffer;
    bool firstShield = true;

    // Start is called before the first frame update
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
        if (GameObject.Find("mainPlayer").GetComponent<PlayerStats>().CanShield() && Input.GetKeyDown(KeyCode.I) && ((Time.time >= shieldTMarker + shieldTBuffer) || (firstShield)))
        {
            if (firstShield) { firstShield = false; }
            shieldTMarker = Time.time;
            StartCoroutine(ShieldCooldownTimer());
        }
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
