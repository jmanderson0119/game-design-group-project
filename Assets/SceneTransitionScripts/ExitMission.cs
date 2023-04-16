using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMission : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
 {
     if (other.gameObject.tag == "Player")
         SceneManager.LoadScene (0);
 }
}