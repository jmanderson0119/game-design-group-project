using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private PlayerStats player;
    private float playerHealth;

    [SerializeField] private GameObject fullheartPrefab;
    [SerializeField] private GameObject emptyheartPrefab;
    private GameObject fullHeart;
    private GameObject emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("mainPlayer").GetComponent<PlayerStats>();
        playerHealth = player.Health();

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
        
        if (playerHealth > player.Health())
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
