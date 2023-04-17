using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerV1 : MonoBehaviour
{
    //The idea is to put this in invisible GameObjects named SpawnPointn
    //n being the number of the Spawnpoint of SpawnPoints in the room

    //MonsterSpawnerV1 spawns all the enemies at once
    
    public GameObject enemyPrefab;
    public GameObject[] spawnPoints;
    public int n = 0;
    public int hunting;
    public int hunted = 0;

    void Start()
    {
        //Spawns in the total amount of enemies assigned to the room at once
        for (int i = 0; i < hunting; i++) {
            Instantiate(enemyPrefab, spawnPoints[n].transform.position, Quaternion.identity);
            n = n + 1;
        }
        
    }
}
