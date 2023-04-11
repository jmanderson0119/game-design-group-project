using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float susLevel = 0.15f;
    public float maxVision = 5f;
    public Vector3 playerLastPosition;
    private float actionTimePlaned;
    private string[] currentAction;
    private float idleMaxTime = 1f;
    private float movingSpeed = 1.5f;
    public float maxAttackRange = 1.5f;
    public float k_val = 5f;
    public float w_val = 0.25f;
    private int LayerPlayer;
    public int health = 6;
    public float intervalBetweenAttacks = 1f;
    private float chargingTime = 0f;
    [SerializeField] GameObject questionMark;
    private GameObject questionMarkClone;
    [SerializeField] GameObject smallAlert;
    private GameObject smallAlertClone;
    [SerializeField] GameObject bigAlert;
    private GameObject bigAlertClone;

    Rigidbody2D enemy;
    Animator animator;


    // This function is a logistic model that calculates the susLevel for the next frame. The function is bounded by 0 and 1
    // susLevel -> 1, more sus
    // susLevel -> 0, less sus
    // Effect of seeing player reduces as the distance increases
    void susUpdate(float d, float k, float w, float deltaT, bool seePlayer)
    {
        float d_diff;
        if (seePlayer)
        {
            d_diff = maxVision - Mathf.Min(d, maxVision);
        }
        else
        {
            d_diff = 0;
        }
        float c = susLevel / (1 - susLevel);
        float z = c * Mathf.Exp((k * d_diff / maxVision - w) * deltaT);
        susLevel = Mathf.Min(0.95f, Mathf.Max(0.15f, z / (1 + z)));
    }

    // Raycasting here
    float distanceToObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 1f, Vector2.down); ;
        if (currentAction[1] == "left")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.left * 1f, Vector2.left);
        }
        else if (currentAction[1] == "right")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.right * 1f, Vector2.right);
        }
        else if (currentAction[1] == "up")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.up * 1f, Vector2.up);
        }
        else if (currentAction[1] == "down")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.down * 1f, Vector2.down);
        }

        float distance = 0;
        if (hit.collider != null)
        {
            if (currentAction[1] == "left" || currentAction[1] == "right")
            {
                distance = Mathf.Abs(hit.point.x - transform.position.x);
            }
            else
            {
                distance = Mathf.Abs(hit.point.y - transform.position.y);
            }
        }

        //Debug.Log(hit.collider.gameObject.name);
        return (distance / 2f);
    }

    // Not finish yet
    // Enemy should spot player after receiving damage from player
    bool spotPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 1f, Vector2.down); ;
        if (currentAction[1] == "left")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.left * 1f, Vector2.left);
        }
        else if (currentAction[1] == "right")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.right * 1f, Vector2.right);
        }
        else if (currentAction[1] == "up")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.up * 1f, Vector2.up);
        }
        else if (currentAction[1] == "down")
        {
            hit = Physics2D.Raycast(transform.position + Vector3.down * 1f, Vector2.down);
        }

        float distance = maxVision + 5f;
        if (hit.collider != null)
        {
            if (currentAction[1] == "left" || currentAction[1] == "right")
            {
                distance = Mathf.Abs(hit.point.x - transform.position.x);
            }
            else
            {
                distance = Mathf.Abs(hit.point.y - transform.position.y);
            }

            if (hit.transform.gameObject.layer == LayerPlayer)
            {
                if (distance <= maxVision)
                {
                    playerLastPosition = hit.transform.position;
                    return true;
                }
            }
        }
        return false;
    }

    void randomAction(int state)
    {
        float probIdle = 0f;
        float probLeft = 0f;
        float probRight = 0f;
        float probUp = 0f;
        float probDown = 0f;
        float distToPlayer = Mathf.Infinity;
        // state = 0, small sus 
        // state = 1, medium sus
        // state = 2, high sus
        if (state == 0)
        {
            probIdle = 0.5f;
            probLeft = 0.25f;
            probRight = 0.25f;
            probUp = 0.25f;
            probDown = 0.25f;
        }
        else if (state == 1)
        {
            probIdle = 0.2f;
            if (playerLastPosition != null)
            {
                float xDiff = transform.position.x - playerLastPosition.x;
                float yDiff = transform.position.y - playerLastPosition.y;
                distToPlayer = (yDiff / (xDiff + yDiff)) * yDiff + (xDiff / (xDiff + yDiff));
                if (xDiff >= 0)
                {
                    if (yDiff >= 0)
                    {
                        probUp = 0.15f;
                        probRight = 0.15f;
                        probDown = yDiff / (xDiff + yDiff) * 0.7f;
                        probLeft = xDiff / (xDiff + yDiff) * 0.7f;
                    }
                    else
                    {
                        probDown = 0.15f;
                        probRight = 0.15f;
                        probUp = yDiff / (xDiff + yDiff) * 0.7f;
                        probLeft = xDiff / (xDiff + yDiff) * 0.7f;
                    }
                }
                else
                {
                    if (yDiff >= 0)
                    {
                        probUp = 0.15f;
                        probLeft = 0.15f;
                        probDown = yDiff / (xDiff + yDiff) * 0.7f;
                        probRight = xDiff / (xDiff + yDiff) * 0.7f;
                    }
                    else
                    {
                        probDown = 0.15f;
                        probLeft = 0.15f;
                        probUp = yDiff / (xDiff + yDiff) * 0.7f;
                        probRight = xDiff / (xDiff + yDiff) * 0.7f;
                    }
                }
            }
            else
            {
                probLeft = 0.25f;
                probRight = 0.25f;
                probUp = 0.25f;
                probDown = 0.25f;
            }
        }
        else if (state == 2)
        {
            probIdle = 0.1f;
            if (playerLastPosition != null)
            {
                float xDiff = transform.position.x - playerLastPosition.x;
                float yDiff = transform.position.y - playerLastPosition.y;
                distToPlayer = (yDiff / (xDiff + yDiff)) * yDiff + (xDiff / (xDiff + yDiff));
                if (xDiff >= 0)
                {
                    if (yDiff >= 0)
                    {
                        probUp = 0.05f;
                        probRight = 0.05f;
                        probDown = yDiff / (xDiff + yDiff) * 0.9f;
                        probLeft = xDiff / (xDiff + yDiff) * 0.9f;
                    }
                    else
                    {
                        probDown = 0.05f;
                        probRight = 0.05f;
                        probUp = yDiff / (xDiff + yDiff) * 0.9f;
                        probLeft = xDiff / (xDiff + yDiff) * 0.9f;
                    }
                }
                else
                {
                    if (yDiff >= 0)
                    {
                        probUp = 0.05f;
                        probLeft = 0.05f;
                        probDown = yDiff / (xDiff + yDiff) * 0.9f;
                        probRight = xDiff / (xDiff + yDiff) * 0.9f;
                    }
                    else
                    {
                        probDown = 0.05f;
                        probLeft = 0.05f;
                        probUp = yDiff / (xDiff + yDiff) * 0.9f;
                        probRight = xDiff / (xDiff + yDiff) * 0.9f;
                    }
                }
            }
            else
            {
                probLeft = 0.25f;
                probRight = 0.25f;
                probUp = 0.25f;
                probDown = 0.25f;
            }
        }


        float q1 = Random.Range(0f, 1f);
        float q2 = Random.Range(0f, 1f);

        // Determine direction first to shoot raycast
        if (q2 < probLeft)
        {
            currentAction[1] = "left";
        }
        else if (q2 < probLeft + probRight && q2 >= probLeft)
        {
            currentAction[1] = "right";
        }
        else if (q2 < probLeft + probRight + probUp && q2 >= probLeft + probRight)
        {
            currentAction[1] = "up";
        }
        else if (q2 <= 1 && q2 >= probLeft + probRight + probUp)
        {
            currentAction[1] = "down";
        }

        if (q1 < probIdle)
        {
            currentAction[0] = "idle";
            // Idle action time. Adjusted arbitrarily
            actionTimePlaned = Random.Range(0f, idleMaxTime);
        }
        else
        {
            currentAction[0] = "move";
            // May adjust this to prevent hitting into wall 
            // use min here to prevent overshooting
            float maxTimeTravel = Mathf.Min(distanceToObstacle(), distToPlayer) / movingSpeed * 1f;
            actionTimePlaned = Random.Range(0f, maxTimeTravel);
        }
    }

    void move()
    {
        Vector2 velocity = new Vector2(0, 0);
        if (currentAction[0] == "idle")
        {
            // Use animation to achieve rotation
            // Do this later
            if (currentAction[1] == "left")
            {
                animator.SetBool("left", true);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
            }
            else if (currentAction[1] == "right")
            {
                animator.SetBool("left", false);
                animator.SetBool("right", true);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
            }
            else if (currentAction[1] == "up")
            {
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", true);
                animator.SetBool("down", false);
            }
            else if (currentAction[1] == "down")
            {
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
            }
        }
        else if (currentAction[0] == "move")
        {
            if (currentAction[1] == "left")
            {
                animator.SetBool("left", true);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                //transform.Translate(Vector3.left * Time.deltaTime * movingSpeed);
                velocity = Vector2.left * movingSpeed * Time.deltaTime;
            }
            else if (currentAction[1] == "right")
            {
                animator.SetBool("left", false);
                animator.SetBool("right", true);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                //transform.Translate(Vector3.right * Time.deltaTime * movingSpeed);
                velocity = Vector2.right * movingSpeed * Time.deltaTime;
            }
            else if (currentAction[1] == "up")
            {
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", true);
                animator.SetBool("down", false);
                //transform.Translate(Vector3.up * Time.deltaTime * movingSpeed);
                velocity = Vector2.up * movingSpeed * Time.deltaTime;
            }
            else if (currentAction[1] == "down")
            {
                animator.SetBool("left", false);
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
                //transform.Translate(Vector3.down * Time.deltaTime * movingSpeed);
                velocity = Vector2.down * movingSpeed * Time.deltaTime;
            }
        }
        enemy.MovePosition(enemy.position + velocity);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAction = new string[] { "idle", "down" };
        actionTimePlaned = Random.Range(0f, idleMaxTime);
        LayerPlayer = LayerMask.NameToLayer("Player");
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    // Make sure that an action can be disrupted if the enemy see player or get damaged during an action.
    void FixedUpdate()
    {
        actionTimePlaned = actionTimePlaned - Time.deltaTime;
        bool seePlayer = spotPlayer();
        // Update the sus level
        // Make system dynamic slower
        susUpdate(distanceToObstacle(), k_val, w_val, Time.deltaTime, seePlayer);
        //Debug.Log(Time.deltaTime);
        //Debug.Log(distanceToObstacle());
        //Debug.Log(currentAction[1]);
        //Debug.Log(actionTimePlaned);
        // Use susLevel to separate behaviors
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Application.Quit();
        }
        //Debug.Log(susLevel);
        if (susLevel <= 0.155)
        {
            if (questionMarkClone != null)
            {
                Destroy(questionMarkClone);
            }
            if (smallAlertClone != null)
            {
                Destroy(smallAlertClone);
            }
            if (bigAlertClone != null)
            {
                Destroy(bigAlertClone);
            }
            if (actionTimePlaned > 0)
            {
                move();
            }
            else
            {
                randomAction(0);
                move();
            }
        }
        else if (0.155 < susLevel && susLevel < 0.2)
        {
            if (smallAlertClone != null)
            {
                Destroy(smallAlertClone);
            }
            if (bigAlertClone != null)
            {
                Destroy(bigAlertClone);
            }
            // This checks if questionMark is in the scene or not
            if (questionMarkClone == null)
            {
                questionMarkClone = Instantiate(questionMark, transform.position + new Vector3(0.5f, 0.5f, 0f), transform.rotation, transform);
            }
            // If from last frame to current frame, the actionTimePlaned becomes 0 or negative, the enemy will not perform the last action, which prevents hitting the wall.
            if (actionTimePlaned > 0)
            {
                move();
            }
            else
            {
                randomAction(0);
                move();
            }
        }
        else if (susLevel >= 0.2 && susLevel < 0.7)
        {
            if (questionMarkClone != null)
            {
                Destroy(questionMarkClone);
            }
            if (bigAlertClone != null)
            {
                Destroy(bigAlertClone);
            }
            if (smallAlertClone == null)
            {
                smallAlertClone = Instantiate(smallAlert, transform.position + new Vector3(0.5f, 0.5f, 0f), transform.rotation, transform);
            }
            // Deterministic move
            if (seePlayer)
            {
                actionTimePlaned = 0;
                float xDiff = transform.position.x - playerLastPosition.x;
                float yDiff = transform.position.y - playerLastPosition.y;
                if (Mathf.Abs(xDiff) >= Mathf.Abs(yDiff))
                {
                    if (Mathf.Abs(xDiff) > maxAttackRange)
                    {
                        if (xDiff >= 0)
                        {
                            transform.Translate(Vector3.left * Time.deltaTime * movingSpeed * 1.5f);
                        }
                        else
                        {
                            transform.Translate(Vector3.right * Time.deltaTime * movingSpeed * 1.5f);
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(yDiff) > maxAttackRange)
                    {
                        if (yDiff >= 0)
                        {
                            transform.Translate(Vector3.down * Time.deltaTime * movingSpeed * 1.5f);
                        }
                        else
                        {
                            transform.Translate(Vector3.up * Time.deltaTime * movingSpeed * 1.5f);
                        }
                    }
                }
            }
            else
            {
                if (actionTimePlaned > 0)
                {
                    move();
                }
                else
                {
                    randomAction(1);
                    move();
                }
            }
        }
        else if (susLevel >= 0.7)
        {
            if (questionMarkClone != null)
            {
                Destroy(questionMarkClone);
            }
            if (smallAlertClone != null)
            {
                Destroy(smallAlertClone);
            }
            if (bigAlertClone == null)
            {
                bigAlertClone = Instantiate(bigAlert, transform.position + new Vector3(0.5f, 0.5f, 0f), transform.rotation, transform);
            }
            if (seePlayer)
            {
                actionTimePlaned = 0;
                float xDiff = transform.position.x - playerLastPosition.x;
                float yDiff = transform.position.y - playerLastPosition.y;
                if (Mathf.Abs(xDiff) >= Mathf.Abs(yDiff))
                {
                    if (Mathf.Abs(xDiff) > maxAttackRange)
                    {
                        if (xDiff >= 0)
                        {
                            transform.Translate(Vector3.left * Time.deltaTime * movingSpeed * 3f);
                        }
                        else
                        {
                            transform.Translate(Vector3.right * Time.deltaTime * movingSpeed * 3f);
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(yDiff) > maxAttackRange)
                    {
                        if (yDiff >= 0)
                        {
                            transform.Translate(Vector3.down * Time.deltaTime * movingSpeed * 3f);
                        }
                        else
                        {
                            transform.Translate(Vector3.up * Time.deltaTime * movingSpeed * 3f);
                        }
                    }
                }

                if (Mathf.Max(Mathf.Abs(xDiff), Mathf.Abs(yDiff)) <= maxAttackRange && chargingTime == 0f)
                {
                    animator.SetBool("attack", true);
                    chargingTime = chargingTime + Time.deltaTime;
                    Debug.Log(LayerPlayer);
                    Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, maxAttackRange);

                    foreach (Collider2D target in hitTargets)
                    {
                        // Change name if name is not mainPlayer
                        if (target.name == "mainPlayer")
                        {
                            target.gameObject.GetComponent<PlayerStats>().IncHealth(-1.0f);
                        }
                    }
                }
                else if (chargingTime > 0 && chargingTime <= intervalBetweenAttacks * 0.5f)
                {
                    animator.SetBool("attack", false);
                    chargingTime = chargingTime + Time.deltaTime;
                }
                else if (chargingTime > intervalBetweenAttacks * 0.5f && chargingTime < intervalBetweenAttacks)
                {
                    chargingTime = chargingTime + Time.deltaTime;
                }
                else if (chargingTime > intervalBetweenAttacks)
                {
                    chargingTime = 0f;
                }
                else
                {
                    animator.SetBool("attack", false);
                }
            }
            else
            {
                if (actionTimePlaned > 0)
                {
                    move();
                }
                else
                {
                    randomAction(1);
                    move();
                }
            }
        }
    }
}
