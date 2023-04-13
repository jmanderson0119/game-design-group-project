using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyRequest : MonoBehaviour
{
    public MonsterSpawnerV1 spawner;
    public void EasyGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject Spawn =GameObject.FindWithTag("Spawn");
        spawner=Spawn.GetComponent<MonsterSpawnerV1>();
        spawner.hunting = 1;
    }
}
