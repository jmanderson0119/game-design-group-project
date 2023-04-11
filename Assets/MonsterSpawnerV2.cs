using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerV2 : MonoBehaviour
{
    //MonsterSpawnerV2 spawns enemies over time at random points across the map
    //Makes sure that the # of enemies spawned doesn't go over the set number of enemies
    //Assigned to a room
    public int n;
    public int hunting;
    public float spawnRate;
    public int hunted = 0;

    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;

    void Start() { 
        StartCoroutine(SpawnEnemies()); 
    }

    IEnumerator SpawnEnemies()
    {
        
        for(int i = 0; i < hunting; i++){
            Debug.Log("Loop ran");
            int r = Random.Range(0, n);
            Debug.Log("Random Number : " + r);
            Instantiate(enemyPrefab, spawnPoints[r].transform.position, Quaternion.identity);
            Debug.Log("Enemy spawned");
            yield return new WaitForSeconds(spawnRate);
            Debug.Log("Waited");
        }
        StopCoroutine(SpawnEnemies());
    }
}
