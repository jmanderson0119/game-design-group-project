using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        // non ally hit main player
        if (col.gameObject.name != transform.parent.name)
        {
            if (col.gameObject.name == "mainPlayer" && !transform.parent.GetComponent<EnemyBehavior>().ally)
            {
                col.gameObject.GetComponent<PlayerStats>().IncHealth(-1.0f * transform.parent.GetComponent<EnemyBehavior>().enemyLevel);
            }
            else if ((transform.parent.GetComponent<EnemyBehavior>().ally && !col.gameObject.GetComponent<EnemyBehavior>().ally) || (!transform.parent.GetComponent<EnemyBehavior>().ally && col.gameObject.GetComponent<EnemyBehavior>().ally))
            {
                col.gameObject.GetComponent<EnemyBehavior>().health -= 1 * transform.parent.GetComponent<EnemyBehavior>().enemyLevel;
            }
        }
    }
}
