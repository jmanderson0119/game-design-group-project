using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
 {
     if (other.gameObject.tag == "Player")
         Application.Quit();
         Debug.Log("Quit)");
 }
}
