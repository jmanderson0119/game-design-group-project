using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    static bool first = true;
    [SerializeField] public static SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(first);
        if(first == true){
            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Intro";
            GameObject oldPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
            oldPlayer.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(Wait10());
            first = false;
        }
    }

    IEnumerator Wait10(){
        yield return new WaitForSeconds(10);
        this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "LowResBackground";
        GameObject oldPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
        oldPlayer.GetComponent<PlayerMovement>().enabled = true;
    }

}
