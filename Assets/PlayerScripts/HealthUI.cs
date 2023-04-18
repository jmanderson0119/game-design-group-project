using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private PlayerStats player;
    private float playerHealth;
    private Vector3 healthBar;
    private Vector3 emptyBar;

    [SerializeField] private GameObject fullheartPrefab;
    [SerializeField] private GameObject emptyheartPrefab;
    private GameObject fullHeart;
    private GameObject emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("mainPlayer").GetComponent<PlayerStats>();
        playerHealth = player.Health();
        healthBar = new Vector3(100, 300, 0);
        emptyBar = new Vector3(100, 300, 10);

        for (int i = 0; i < playerHealth; i++)
        {
            emptyHeart = Instantiate(emptyheartPrefab) as GameObject;
            fullHeart = Instantiate(fullheartPrefab) as GameObject;
            fullHeart.name = "FullHeart" + (i + 1);
            emptyHeart.transform.parent = gameObject.transform;
            fullHeart.transform.parent = gameObject.transform;
            emptyHeart.transform.position = emptyBar;
            fullHeart.transform.position = healthBar;
            emptyBar.x += 30;
            healthBar.x += 30;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerHealth != player.Health())
        {
            float healthDifference = playerHealth - player.Health(); 
            
            for (int j = 0; j < healthDifference; j++)
            {
                Destroy(GameObject.Find("FullHeart" + (playerHealth - j)));
            }
            playerHealth = player.Health();
        }
    }
}
