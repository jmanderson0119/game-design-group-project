using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;
    private GameObject shield;

    private float shieldTBuffer;
    private float shieldTMarker;

    private bool shieldUp;
    bool firstShield = true;



    // Start is called before the first frame update
    void Start()
    {
        shieldTBuffer = gameObject.GetComponent<PlayerStats>().ShieldTBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && ((Time.time >= shieldTMarker + shieldTBuffer) || (firstShield)) && gameObject.GetComponent<PlayerStats>().CanShield())
        {
            if (firstShield) { firstShield = false; }
            shieldTMarker = Time.time;
            StartCoroutine(ShieldTimer());
            shield = Instantiate(shieldPrefab) as GameObject;
            shield.transform.position = transform.position;
        }

        if (shieldUp)
        {
            shield.transform.position = transform.position;
        }
    }

    IEnumerator ShieldTimer()
    {
        shieldUp = true;
        gameObject.GetComponent<PlayerStats>().Dash(false);
        gameObject.GetComponent<PlayerStats>().Shoot(false);
        gameObject.GetComponent<PlayerStats>().Melee(false);
        gameObject.GetComponent<PlayerStats>().DamageState(false);
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<PlayerStats>().DamageState(true);
        gameObject.GetComponent<PlayerStats>().Dash(true);
        gameObject.GetComponent<PlayerStats>().Shoot(true);
        gameObject.GetComponent<PlayerStats>().Melee(true);
        Destroy(shield);
        shieldUp = false;
    }
}
