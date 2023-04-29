using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [SerializeField] public GameObject creditPage;
    void OnCollisionEnter2D(Collision2D other)
 {
     if (other.gameObject.tag == "Player")
        Credit();
 }

    void Credit()
    {
        creditPage.GetComponent<SpriteRenderer>().sortingLayerName = "Intro";
        Wait5();
    }

    IEnumerator Wait5(){
        yield return new WaitForSeconds(5);
         Application.Quit();
         Debug.Log("Quit)");
    }
}
