using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is used for displaying the health bar within the level
public class HealthUI : MonoBehaviour
{
    private PlayerStats player; // accessor for tracked player stats
    private float playerHealth; // accesses player's default health stat

    // health visuals
    [SerializeField] private GameObject fullheartPrefab; 
    [SerializeField] private GameObject emptyheartPrefab; 
    private GameObject fullHeart;
    private GameObject emptyHeart;

    // sfx for when the player is damaged
    private AudioSource damageSource;
    [SerializeField] private AudioClip damageNoise;

    // these three work in tandem to create the damaged effect when the player is hit
    private SpriteRenderer playerSprite;
    [SerializeField] private Color damageColor;
    private Color playerDefaultColor;

    // Start is called before the first frame update
    void Start()
    {
        //initialization of germane player data
        player = GameObject.Find("mainPlayer").GetComponent<PlayerStats>();
        playerHealth = player.Health();
        damageSource = GameObject.Find("mainPlayer").GetComponent<AudioSource>();
        playerSprite = GameObject.Find("mainPlayer").GetComponent<SpriteRenderer>();
        playerDefaultColor = playerSprite.material.color;

        // instantiates and places heart prefabs based on player's health stat
        for (int i = 0; i < playerHealth; i++)
        {
            emptyHeart = Instantiate(emptyheartPrefab) as GameObject;
            fullHeart = Instantiate(fullheartPrefab) as GameObject;
            fullHeart.name = "FullHeart" + (i + 1);
            emptyHeart.transform.SetParent(gameObject.transform, false);
            fullHeart.transform.SetParent(gameObject.transform, false);
            emptyHeart.transform.position = new Vector3(emptyHeart.transform.position.x + 27 * (i + 1), emptyHeart.transform.position.y, emptyHeart.transform.position.z);
            fullHeart.transform.position = new Vector3(fullHeart.transform.position.x + 27 * (i + 1), fullHeart.transform.position.y, fullHeart.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
            
        // health bar updated whenever the player takes damage
        if (playerHealth > player.Health())
        {
            
            StartCoroutine(SpriteColor()); // starts the visual cue that the player has taken damage
            damageSource.PlayOneShot(damageNoise, 1f); // plays the audio cue theat the player has taken damage

            float healthDifference = playerHealth - player.Health(); // used for handling health bar behavior
            
            // removes full heart prefabs equal to the amount of damage dealt
            for (int j = 0; j < healthDifference; j++)
            {
                Destroy(GameObject.Find("FullHeart" + (playerHealth - j)));
            }
            playerHealth = player.Health();
        }
    }
    
    // creates the visual cue for player damage
    IEnumerator SpriteColor()
    {
        playerSprite.material.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        playerSprite.material.color = playerDefaultColor;
        yield return new WaitForSeconds(0.1f);
        playerSprite.material.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        playerSprite.material.color = playerDefaultColor;

    }
}
