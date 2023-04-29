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
                if (col.gameObject.GetComponent<PlayerStats>().Damageable())
                {
                    col.gameObject.GetComponent<PlayerStats>().IncHealth(-1.0f * EnemyBehavior.enemyLevel);
                }
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if ((transform.parent.GetComponent<EnemyBehavior>().ally && !col.gameObject.GetComponent<EnemyBehavior>().ally) || (!transform.parent.GetComponent<EnemyBehavior>().ally && col.gameObject.GetComponent<EnemyBehavior>().ally))
                {
                    col.gameObject.GetComponent<EnemyBehavior>().health -= 1 * EnemyBehavior.enemyLevel;
                    col.gameObject.GetComponent<EnemyBehavior>().damage();
                }
            }
        }
    }
}
