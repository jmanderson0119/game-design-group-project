using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "mainPlayer")
        {
            transform.parent.GetComponent<EnemyBehavior>().CollisionDetected(this);
        }
    }
}
