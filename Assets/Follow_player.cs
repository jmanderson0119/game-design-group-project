using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Follow_player : MonoBehaviour {

    public Transform player;

    // Update is called once per frame
    void Update () {
        transform.position = player.transform.position + new Vector3(0, 1, -25);
    }
}