using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MediumRequest : MonoBehaviour
{
    public MonsterSpawnerV1 spawner;
    public void MediumGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject Spawn =GameObject.FindWithTag("Spawn");
        spawner=Spawn.GetComponent<MonsterSpawnerV1>();
        spawner.hunting = 2;
    }
}
