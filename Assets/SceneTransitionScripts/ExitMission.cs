using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMission : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
 {
     if (other.gameObject.tag == "Player")
        if(GameObject.Find("EnemySpawners")==null){
            SceneManager.LoadScene (0);
        }
        else
        {
            GameObject.Find("EnemySpawners").GetComponent<MonsterCounter>().EarlyComplete();
        }
        

 }
}
