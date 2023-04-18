using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        // non ally hit main player
        if (col.gameObject.name != transform.parent.name)
        {
            if (col.gameObject.name == "mainPlayer")
            {
                if (col.gameObject.GetComponent<PlayerStats>().Damageable())
                {
                    col.gameObject.GetComponent<PlayerStats>().IncHealth(-4f);
                }
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                col.gameObject.GetComponent<EnemyBehavior>().health -= 4;
            }
        }
    }
}
