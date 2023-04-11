using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float speed = 0.01f;
    private Rigidbody2D _Rigidbody;
    private Vector2 velocity;
    public int health = 10;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        velocity = (new Vector2(inputX, inputY)).normalized * speed;
        /*
        if (Input.GetKeyDown())
        { 
            
        }
        */

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate ()
    {
        _Rigidbody.velocity = velocity;
    }
}
