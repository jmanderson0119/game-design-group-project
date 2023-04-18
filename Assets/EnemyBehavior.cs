using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float susLevel = 0.15f;
    public float maxVision = 5f;
    public Vector3 playerLastPosition;
    public Vector3 targetEnemyLastPosition;
    private float actionTimePlaned;
    private string[] currentAction;
    private float idleMaxTime = 1f;
    private float movingSpeed = 1.5f;
    public float maxAttackRange = 1.5f;
    public float oldMaxAttackRange = 1.5f;
    public float k_val = 5f;
    public float w_val = 0.25f;
    private int LayerPlayer;
    private int LayerEnemy;
    public int health = 6;
    public float intervalBetweenAttacks = 1f;
    private float chargingTime = 0f;
    private float castingTime = 0f;
    private float lightingCastingTime = 0f;
    public float intervalBetweenLighting = 5f;
    public float bossDashCastingTime = 1.5f;
    [SerializeField] GameObject questionMark;
    private GameObject questionMarkClone;
    [SerializeField] GameObject smallAlert;
    private GameObject smallAlertClone;
    [SerializeField] GameObject bigAlert;
    private GameObject bigAlertClone;
    private GameObject levelMarkClone;
    [SerializeField] GameObject levelMark;
    private GameObject boneClone;
    [SerializeField] GameObject bone;
    [SerializeField] GameObject pChar;
    private GameObject pCharClone;
    private GameObject levelClone;
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;
    private GameObject player;
    public bool ally = false;
    private bool move2Player = false;
    public int enemyLevel = 1;
    public float allyStayRadius = 2f;

    public bool isLargePumpkin = false;
    public bool isSmallPumpkin = false;
    public bool isEye = false;
    public bool isSkull = false;
    public bool isBoss = false;
    public bool isSkullSoldier = false;

    private Queue<GameObject> pumpkinSeeds = new Queue<GameObject>();
    private Queue<GameObject> skullHeadBullets = new Queue<GameObject>();

    private bool DestroySeed_running = false;
    private bool DestroySkullHeadBullet_running = false;
    private bool isTransformed = false;
    private Vector3 oldScale;
    float oldIntervalBetweenAttacks = 1f;
    float oldMovingSpeed = 1.5f;
    int oldHealth = 6;

    [SerializeField] GameObject pumpkinSeed;
    [SerializeField] GameObject skullHeadBullet;
    [SerializeField] GameObject smallPumpkin;

    // This indicates whether this enemy can be pet or not
    // eye is not petable
    private bool petable = false;

    public bool isBossActive = false;

    [SerializeField] GameObject lighting;
    private GameObject lightingClone1;
    private GameObject lightingClone2;
    private GameObject lightingClone3;
    private GameObject lightingClone4;

    [SerializeField] GameObject lightingShadow;
    private GameObject lightingShadowClone1;
    private GameObject lightingShadowClone2;
    private GameObject lightingShadowClone3;
    private GameObject lightingShadowClone4;

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
        if (isBoss)
        {
            if (currentAction[1] == "left")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.left * 2f, Vector2.left);
            }
            else if (currentAction[1] == "right")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.right * 2f, Vector2.right);
            }
            else if (currentAction[1] == "up")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.up * 2f, Vector2.up);
            }
            else if (currentAction[1] == "down")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.down * 2f, Vector2.down);
            }
        }
        else
        {
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
        return (distance);
    }

    // Not finish yet
    // Enemy should spot player after receiving damage from player
    bool spotTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * 1f, Vector2.down);
        if (isBoss)
        {
            if (currentAction[1] == "left")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.left * 3f, Vector2.left);
            }
            else if (currentAction[1] == "right")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.right * 3f, Vector2.right);
            }
            else if (currentAction[1] == "up")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.up * 3f, Vector2.up);
            }
            else if (currentAction[1] == "down")
            {
                hit = Physics2D.Raycast(transform.position + Vector3.down * 3f, Vector2.down);
            }
        }
        else
        {
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

            if (hit.transform.gameObject.layer == LayerPlayer && !ally)
            {
                if (distance <= maxVision)
                {
                    playerLastPosition = hit.transform.position;
                    if (petable)
                    {
                        move2Player = true;
                    }
                    else
                    { 
                        move2Player = false;
                    }
                    return true;
                }
            }
            else if (hit.transform.gameObject.layer == LayerEnemy && ally) // an ally spots a non-ally
            {
                if (distance <= maxVision)
                {
                    if (!hit.transform.gameObject.GetComponent<EnemyBehavior>().ally)
                    {
                        targetEnemyLastPosition = hit.transform.position;
                        move2Player = false;
                        return true;
                    }
                }
            }
            else if (hit.transform.gameObject.layer == LayerEnemy && !ally) // a non-ally spots an ally
            {
                if (distance <= maxVision)
                {
                    if (hit.transform.gameObject.GetComponent<EnemyBehavior>().ally)
                    {
                        targetEnemyLastPosition = hit.transform.position;
                        move2Player = false;
                        return true;
                    }
                }
            }
            else if (hit.transform.gameObject.layer != LayerEnemy && hit.transform.gameObject.layer != LayerPlayer && ally)
            {
                playerLastPosition = player.transform.position;
                move2Player = true;
                return false;
            }
            else if (hit.transform.gameObject.layer == LayerPlayer && ally)
            {
                playerLastPosition = player.transform.position;
                move2Player = true;
                return true;
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
        Vector3 lastPosition = ally && !move2Player ? targetEnemyLastPosition : playerLastPosition;
        //Debug.Log(ally && !move2Player);
        //Debug.Log(lastPosition);
        // state = 0, small sus 
        // state = 1, medium sus
        // state = 2, high sus
        float distance2Player = 0f;
        if (ally && move2Player)
        {
            float xDiff = transform.position.x - lastPosition.x;
            float yDiff = transform.position.y - lastPosition.y;
            distance2Player = Mathf.Sqrt(Mathf.Pow(xDiff, 2f) + Mathf.Pow(yDiff, 2f));
        }


        if (ally && move2Player && distance2Player > allyStayRadius)
        {
            probIdle = 0f;
            float xDiff = transform.position.x - lastPosition.x;
            float yDiff = transform.position.y - lastPosition.y;
            distToPlayer = (Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff))) * Mathf.Abs(yDiff) + (Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff))) * Mathf.Abs(xDiff);
            //distToPlayer = Mathf.Min(Mathf.Abs(xDiff), Mathf.Abs(yDiff));
            if (xDiff >= 0)
            {
                if (yDiff >= 0)
                {
                    probUp = 0.15f;
                    probRight = 0.15f;
                    probDown = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                    probLeft = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                }
                else
                {
                    probDown = 0.15f;
                    probRight = 0.15f;
                    probUp = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                    probLeft = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                }
            }
            else
            {
                if (yDiff >= 0)
                {
                    probUp = 0.15f;
                    probLeft = 0.15f;
                    probDown = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                    probRight = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                }
                else
                {
                    probDown = 0.15f;
                    probLeft = 0.15f;
                    probUp = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                    probRight = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                }
            }
        }
        else
        {
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
                if (lastPosition != null)
                {
                    float xDiff = transform.position.x - lastPosition.x;
                    float yDiff = transform.position.y - lastPosition.y;
                    distToPlayer = (Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff))) * Mathf.Abs(yDiff) + (Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff))) * Mathf.Abs(xDiff);
                    //distToPlayer = Mathf.Min(Mathf.Abs(xDiff), Mathf.Abs(yDiff));
                    if (xDiff >= 0)
                    {
                        if (yDiff >= 0)
                        {
                            probUp = 0.15f;
                            probRight = 0.15f;
                            probDown = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                            probLeft = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                        }
                        else
                        {
                            probDown = 0.15f;
                            probRight = 0.15f;
                            probUp = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                            probLeft = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                        }
                    }
                    else
                    {
                        if (yDiff >= 0)
                        {
                            probUp = 0.15f;
                            probLeft = 0.15f;
                            probDown = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                            probRight = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                        }
                        else
                        {
                            probDown = 0.15f;
                            probLeft = 0.15f;
                            probUp = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
                            probRight = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.7f;
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
                if (lastPosition != null)
                {
                    float xDiff = transform.position.x - lastPosition.x;
                    float yDiff = transform.position.y - lastPosition.y;
                    distToPlayer = (Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff))) * Mathf.Abs(yDiff) + (Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff))) * Mathf.Abs(xDiff);
                    //distToPlayer = Mathf.Min(Mathf.Abs(xDiff), Mathf.Abs(yDiff));
                    if (xDiff >= 0)
                    {
                        if (yDiff >= 0)
                        {
                            probUp = 0.05f;
                            probRight = 0.05f;
                            probDown = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
                            probLeft = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
                        }
                        else
                        {
                            probDown = 0.05f;
                            probRight = 0.05f;
                            probUp = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
                            probLeft = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
                        }
                    }
                    else
                    {
                        if (yDiff >= 0)
                        {
                            probUp = 0.05f;
                            probLeft = 0.05f;
                            probDown = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
                            probRight = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
                        }
                        else
                        {
                            probDown = 0.05f;
                            probLeft = 0.05f;
                            probUp = Mathf.Abs(yDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
                            probRight = Mathf.Abs(xDiff) / (Mathf.Abs(xDiff) + Mathf.Abs(yDiff)) * 0.9f;
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
            //Debug.Log(distToPlayer);
            //Debug.Log(distanceToObstacle());
            float maxTimeTravel = Mathf.Min(distanceToObstacle(), distToPlayer) / movingSpeed;
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
        LayerEnemy = LayerMask.NameToLayer("Enemy");
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        oldMaxAttackRange = maxAttackRange;
        oldScale = transform.localScale;
        oldIntervalBetweenAttacks = intervalBetweenAttacks;
        oldMovingSpeed = movingSpeed;
        oldHealth = health;

        if (enemyLevel == 1)
        {
            Instantiate(level1, transform.position + new Vector3(-1f, -1f, 0f), transform.rotation, transform);
        }
        else if (enemyLevel == 2)
        {
            Instantiate(level2, transform.position + new Vector3(-1f, -1f, 0f), transform.rotation, transform);
        }
        else if (enemyLevel == 3)
        {
            Instantiate(level3, transform.position + new Vector3(-1f, -1f, 0f), transform.rotation, transform);
        }

        // Higher level skull attacks faster
        if (isSkull)
        {
            intervalBetweenAttacks = intervalBetweenAttacks / enemyLevel;
        }

        // Check if this enemy is petable
        player = GameObject.Find("mainPlayer");
        if (player != null && !isEye && !isBoss)
        {
            // Modify this if there is another way to extract reputation
            // Here, I assume that reputation ranges from 0 to 1
            float playerReputation = player.GetComponent<PlayerStats>().Reputation();
            // May consider add petting as a skill
            // Allow player to pet only if he acquires the skill
            if (playerReputation < 50f)
            {
                if (enemyLevel == 1)
                { 
                    petable = true;
                }
            }
            else if (playerReputation >= 50f && playerReputation < 75f)
            {
                if (enemyLevel <= 2)
                {
                    petable = true;
                }
            }
            else if (playerReputation >= 75f)
            {
                if (enemyLevel <= 3)
                {
                    petable = true;
                }
            }

            if (petable)
            {
                boneClone = Instantiate(bone, transform.position + new Vector3(0f, 0.8f, 0f), transform.rotation, transform);
            }
        }
    }

    IEnumerator DestroySeed()
    {
        DestroySeed_running = true;
        yield return new WaitForSeconds(3f);
        if (pumpkinSeeds.Count > 0)
        {
            Destroy(pumpkinSeeds.Dequeue());
        }
        DestroySeed_running = false;
    }

    IEnumerator DestroySkullHeadBullet()
    {
        DestroySkullHeadBullet_running = true;
        yield return new WaitForSeconds(1.5f);
        if (skullHeadBullets.Count > 0)
        {
            Destroy(skullHeadBullets.Dequeue());
        }
        DestroySkullHeadBullet_running = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "mainPlayer")
        {
            if (col.gameObject.GetComponent<PlayerStats>().Damageable())
            {
                col.gameObject.GetComponent<PlayerStats>().IncHealth(-5.0f);
            }
        }
    }

    /*
    public void CollisionDetected(Bullet BulletScript)
    {
        if (!ally)
        {
            player.GetComponent<PlayerStats>().IncHealth(-1.0f * enemyLevel);
        }
    }
    */

    // Update is called once per frame
    // Make sure that an action can be disrupted if the enemy see player or get damaged during an action.
    void FixedUpdate()
    {
        //Debug.Log(currentAction[1]);
        //Debug.Log(currentAction[0]);
        actionTimePlaned = actionTimePlaned - Time.deltaTime;
        // If enemy is ally, bone stops rotate
        if (ally)
        {
            boneClone.GetComponent<Animator>().SetBool("recruited", true);
            playerLastPosition = player.transform.position;
        }

        bool seeTarget = spotTarget();
        // Update the sus level
        // Make system dynamic slower
        if (!isBoss)
        {
            susUpdate(distanceToObstacle(), k_val, w_val, Time.deltaTime, seeTarget);
        }
        else if (isBoss && isBossActive)
        {
            // Boss knows 
            susLevel = 1f;
            playerLastPosition = player.transform.position;
        }

        if (isSkullSoldier || (isEye && isTransformed) || isSkull || isBoss)
        {
            if (!seeTarget)
            {
                animator.SetBool("attack", false);
            }
        }
        //Debug.Log(Time.deltaTime);
        //Debug.Log(distanceToObstacle());
        //Debug.Log(currentAction[1]);
        //Debug.Log(actionTimePlaned);
        // Use susLevel to separate behaviors
        if (!DestroySeed_running && pumpkinSeeds.Count > 0)
        {
            StartCoroutine(DestroySeed());
        }

        if (!DestroySkullHeadBullet_running && skullHeadBullets.Count > 0)
        {
            StartCoroutine(DestroySkullHeadBullet());
        }

        // Handle bullet/seed hit
        //void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("hit player");
        //}


        if (petable)
        { 
            Vector3 playerPosition = player.transform.position;
            float xDiff = transform.position.x - playerPosition.x;
            float yDiff = transform.position.y - playerPosition.y;
            float distance2Player = Mathf.Sqrt(Mathf.Pow(xDiff, 2f) + Mathf.Pow(yDiff, 2f));
            if (distance2Player <= allyStayRadius && !ally)
            {
                if (pCharClone == null)
                {
                    pCharClone = Instantiate(pChar, transform.position + new Vector3(0f, -1f, 0f), transform.rotation, transform);
                }

                if (Input.GetKeyDown("p"))
                {
                    ally = true;
                }
            }
            else
            {
                if (pCharClone != null)
                {
                    Destroy(pCharClone);
                }
            }
        }
        if (isBoss && isBossActive && !animator.GetBool("bossActive"))
        {
            animator.SetBool("bossActive", true);
        }

        if (health <= 0)
        {
            Die();
        }
        if (isBoss && !isBossActive)
        {

        }
        else
        {
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
                if (seeTarget && move2Player)
                {
                    Vector3 lastPosition = ally && !move2Player ? targetEnemyLastPosition : playerLastPosition;
                    actionTimePlaned = 0;
                    float xDiff = transform.position.x - lastPosition.x;
                    float yDiff = transform.position.y - lastPosition.y;
                    float distance2Player = Mathf.Sqrt(Mathf.Pow(xDiff, 2f) + Mathf.Pow(yDiff, 2f));
                    if (distance2Player > allyStayRadius)
                    {
                        if (Mathf.Abs(xDiff) >= Mathf.Abs(yDiff))
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
                        else
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
                        randomAction(0);
                        move();
                    }
                }
            }
            else if (0.155 < susLevel && susLevel < 0.2)
            {
                // transform ege back
                if (isTransformed)
                {
                    animator.SetBool("transform", false);
                    isTransformed = false;
                    maxAttackRange = oldMaxAttackRange;
                    transform.localScale = oldScale;
                    intervalBetweenAttacks = oldIntervalBetweenAttacks;
                    movingSpeed = oldMovingSpeed;
                }

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
                if (seeTarget && move2Player)
                {
                    Vector3 lastPosition = ally && !move2Player ? targetEnemyLastPosition : playerLastPosition;
                    actionTimePlaned = 0;
                    float xDiff = transform.position.x - lastPosition.x;
                    float yDiff = transform.position.y - lastPosition.y;
                    float distance2Player = Mathf.Sqrt(Mathf.Pow(xDiff, 2f) + Mathf.Pow(yDiff, 2f));
                    if (distance2Player > allyStayRadius)
                    {
                        if (Mathf.Abs(xDiff) >= Mathf.Abs(yDiff))
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
                        else
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
                        randomAction(0);
                        move();
                    }
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
                if (seeTarget)
                {
                    Vector3 lastPosition = ally && !move2Player ? targetEnemyLastPosition : playerLastPosition;
                    actionTimePlaned = 0;
                    float xDiff = transform.position.x - lastPosition.x;
                    float yDiff = transform.position.y - lastPosition.y;

                    float distance2Player = Mathf.Sqrt(Mathf.Pow(xDiff, 2f) + Mathf.Pow(yDiff, 2f));
                    if (move2Player)
                    {
                        if (distance2Player > allyStayRadius)
                        {
                            if (Mathf.Abs(xDiff) >= Mathf.Abs(yDiff))
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
                            else
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
                if (isBoss)
                {
                    // May change this time to increase difficulty
                    if (intervalBetweenLighting > lightingCastingTime && lightingCastingTime >= intervalBetweenLighting * 0.5f)
                    {
                        if (lightingShadowClone1 == null && lightingShadowClone2 == null && lightingShadowClone3 == null && lightingShadowClone4 == null)
                        {
                            lightingShadowClone1 = Instantiate(lightingShadow, player.transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
                            lightingShadowClone2 = Instantiate(lightingShadow, player.transform.position + new Vector3(0f, -1f, 0f), transform.rotation);
                            lightingShadowClone3 = Instantiate(lightingShadow, player.transform.position + new Vector3(1f, 0f, 0f), transform.rotation);
                            lightingShadowClone4 = Instantiate(lightingShadow, player.transform.position + new Vector3(-1f, 0f, 0f), transform.rotation);
                        }
                    }
                    else if (lightingCastingTime >= intervalBetweenLighting && lightingCastingTime < intervalBetweenLighting + 0.5f)
                    {
                        if (lightingClone1 == null && lightingClone2 == null && lightingClone3 == null && lightingClone4 == null)
                        {
                            lightingClone1 = Instantiate(lighting, lightingShadowClone1.transform.position, transform.rotation);
                            lightingClone2 = Instantiate(lighting, lightingShadowClone2.transform.position, transform.rotation);
                            lightingClone3 = Instantiate(lighting, lightingShadowClone3.transform.position, transform.rotation);
                            lightingClone4 = Instantiate(lighting, lightingShadowClone4.transform.position, transform.rotation);
                        }
                        Destroy(lightingShadowClone1);
                        Destroy(lightingShadowClone2);
                        Destroy(lightingShadowClone3);
                        Destroy(lightingShadowClone4);
                        //Destroy(lightingClone1);
                        //Destroy(lightingClone2);
                        //Destroy(lightingClone3);
                        //Destroy(lightingClone4);
                    }
                    else if (lightingCastingTime >= intervalBetweenLighting + 1f)
                    {
                        Destroy(lightingClone1);
                        Destroy(lightingClone2);
                        Destroy(lightingClone3);
                        Destroy(lightingClone4);
                        lightingCastingTime = 0;
                    }
                    lightingCastingTime += Time.deltaTime;
                }
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
                if (seeTarget)
                {
                    Vector3 lastPosition = ally && !move2Player ? targetEnemyLastPosition : playerLastPosition;
                    actionTimePlaned = 0;
                    float xDiff = transform.position.x - lastPosition.x;
                    float yDiff = transform.position.y - lastPosition.y;

                    float xDiffAbs = transform.position.x - lastPosition.x;
                    float yDiffAbs = transform.position.y - lastPosition.y;
                    float distance2Player = Mathf.Sqrt(Mathf.Pow(xDiffAbs, 2f) + Mathf.Pow(yDiffAbs, 2f));

                    if (move2Player)
                    {
                        if (distance2Player > allyStayRadius)
                        {
                            if (Mathf.Abs(xDiff) >= Mathf.Abs(yDiff))
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
                            else
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
                            chargingTime = chargingTime + Time.deltaTime;
                            if (isSkullSoldier)
                            {
                                animator.SetBool("attack", true);
                                //Debug.Log(LayerPlayer);
                                Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, maxAttackRange);

                                foreach (Collider2D target in hitTargets)
                                {
                                    // Change name if name is not mainPlayer
                                    // attack non ally enemy
                                    if (target.name == "mainPlayer")
                                    {
                                        if (target.gameObject.GetComponent<PlayerStats>().Damageable())
                                        {
                                            target.gameObject.GetComponent<PlayerStats>().IncHealth(-1.0f * enemyLevel);
                                        }
                                    }
                                    else if (target.gameObject.GetComponent<EnemyBehavior>() != null && !target.gameObject.GetComponent<EnemyBehavior>().ally && ally)
                                    {
                                        target.gameObject.GetComponent<EnemyBehavior>().health -= 1 * enemyLevel;
                                    }
                                    else if (target.gameObject.GetComponent<EnemyBehavior>() != null && target.gameObject.GetComponent<EnemyBehavior>().ally && !ally)
                                    {
                                        target.gameObject.GetComponent<EnemyBehavior>().health -= 1 * enemyLevel;
                                    }
                                }
                            }
                            if (isLargePumpkin || isSmallPumpkin)
                            {
                                if (currentAction[1] == "left")
                                {
                                    GameObject seed = Instantiate(pumpkinSeed, transform.position + new Vector3(-0.5f, 0f, 0f), transform.rotation, transform);
                                    pumpkinSeeds.Enqueue(seed);
                                    seed.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
                                    seed.transform.parent = transform;
                                }
                                else if (currentAction[1] == "right")
                                {
                                    GameObject seed = Instantiate(pumpkinSeed, transform.position + new Vector3(0.5f, 0f, 0f), transform.rotation, transform);
                                    pumpkinSeeds.Enqueue(seed);
                                    seed.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
                                    seed.transform.parent = transform;
                                }
                                else if (currentAction[1] == "up")
                                {
                                    GameObject seed = Instantiate(pumpkinSeed, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation, transform);
                                    pumpkinSeeds.Enqueue(seed);
                                    seed.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                                    seed.transform.parent = transform;
                                }
                                else if (currentAction[1] == "down")
                                {
                                    GameObject seed = Instantiate(pumpkinSeed, transform.position + new Vector3(0f, -0.5f, 0f), transform.rotation, transform);
                                    pumpkinSeeds.Enqueue(seed);
                                    seed.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
                                    seed.transform.parent = transform;
                                }

                            }
                            if (isEye)
                            {
                                if (!isTransformed)
                                {
                                    animator.SetBool("transform", true);
                                    isTransformed = true;
                                    oldMaxAttackRange = maxAttackRange;
                                    maxAttackRange = 1.5f;
                                    transform.localScale += new Vector3(0.8f, 0.8f, 0.8f) * 0.01f * (float)(100 - player.GetComponent<PlayerStats>().Reputation());
                                    float playerReputation = player.GetComponent<PlayerStats>().Reputation();
                                    // The eye will create more powerful enemy if the player's reputation is low
                                    // Adjust the numbers later. Might be too difficult.
                                    if (playerReputation < 50f)
                                    {
                                        enemyLevel = 3;
                                        intervalBetweenAttacks = intervalBetweenAttacks * 0.3f;
                                        movingSpeed = movingSpeed * 1.5f;
                                    }
                                    else if (playerReputation >= 50f && playerReputation < 75f)
                                    {
                                        enemyLevel = 2;
                                        intervalBetweenAttacks = intervalBetweenAttacks * 0.5f;
                                        movingSpeed = movingSpeed * 1.25f;
                                    }
                                    else if (playerReputation >= 75f)
                                    {
                                        enemyLevel = 1;
                                    }
                                }
                                else
                                {
                                    animator.SetBool("attack", true);
                                    //Debug.Log(LayerPlayer);
                                    Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, maxAttackRange);

                                    foreach (Collider2D target in hitTargets)
                                    {
                                        // Change name if name is not mainPlayer
                                        // attack non ally enemy
                                        if (target.name == "mainPlayer")
                                        {
                                            if (target.gameObject.GetComponent<PlayerStats>().Damageable())
                                            {
                                                target.gameObject.GetComponent<PlayerStats>().IncHealth(Mathf.Round(-1.0f * (float)enemyLevel * 0.01f * (float)(100 - player.GetComponent<PlayerStats>().Reputation())));
                                            }
                                        }
                                        else if (target.gameObject.GetComponent<EnemyBehavior>() != null && !target.gameObject.GetComponent<EnemyBehavior>().ally && ally)
                                        {
                                            target.gameObject.GetComponent<EnemyBehavior>().health -= (int)Mathf.Round(1f * (float)enemyLevel * 0.01f * (float)(100 - player.GetComponent<PlayerStats>().Reputation()));
                                        }
                                        else if (target.gameObject.GetComponent<EnemyBehavior>() != null && target.gameObject.GetComponent<EnemyBehavior>().ally && !ally)
                                        {
                                            target.gameObject.GetComponent<EnemyBehavior>().health -= (int)Mathf.Round(1f * (float)enemyLevel * 0.01f * (float)(100 - player.GetComponent<PlayerStats>().Reputation()));
                                        }
                                    }
                                }
                            }
                            if (isSkull)
                            {
                                animator.SetBool("attack", true);
                                if (currentAction[1] == "left")
                                {
                                    GameObject bullet = Instantiate(skullHeadBullet, transform.position + new Vector3(-0.5f, 0f, 0f), transform.rotation, transform);
                                    bullet.transform.Rotate(0, 0, 180f);
                                    skullHeadBullets.Enqueue(bullet);
                                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
                                    bullet.transform.parent = transform;
                                }
                                else if (currentAction[1] == "right")
                                {
                                    GameObject bullet = Instantiate(skullHeadBullet, transform.position + new Vector3(0.5f, 0f, 0f), transform.rotation, transform);
                                    skullHeadBullets.Enqueue(bullet);
                                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
                                    bullet.transform.parent = transform;
                                }
                                else if (currentAction[1] == "up")
                                {
                                    GameObject bullet = Instantiate(skullHeadBullet, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation, transform);
                                    bullet.transform.Rotate(0, 0, 90f);
                                    skullHeadBullets.Enqueue(bullet);
                                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
                                    bullet.transform.parent = transform;
                                }
                                else if (currentAction[1] == "down")
                                {
                                    GameObject bullet = Instantiate(skullHeadBullet, transform.position + new Vector3(0f, -0.5f, 0f), transform.rotation, transform);
                                    bullet.transform.Rotate(0, 0, -90f);
                                    skullHeadBullets.Enqueue(bullet);
                                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
                                    bullet.transform.parent = transform;
                                }
                            }
                            if (isBoss)
                            {
                                Debug.Log("see player");
                                animator.SetBool("attack", true);
                                if (castingTime >= bossDashCastingTime)
                                { 
                                    transform.position = player.transform.position;
                                    castingTime = 0;
                                }
                                castingTime = castingTime + Time.deltaTime;

                            }
                        }
                        else if (chargingTime > 0 && chargingTime <= intervalBetweenAttacks * 0.5f)
                        {
                            if (isSkullSoldier || (isEye && isTransformed) || isSkull)
                            {
                                animator.SetBool("attack", false);
                            }
                            chargingTime = chargingTime + Time.deltaTime;
                            if (isBoss)
                            {
                                castingTime = castingTime + Time.deltaTime;
                            }
                        }
                        else if (chargingTime > intervalBetweenAttacks * 0.5f && chargingTime < intervalBetweenAttacks)
                        {
                            if (isSkullSoldier || (isEye && isTransformed) || isSkull)
                            {
                                animator.SetBool("attack", false);
                            }
                            chargingTime = chargingTime + Time.deltaTime;
                            if (isBoss)
                            {
                                castingTime = castingTime + Time.deltaTime;
                            }
                        }
                        else if (chargingTime > intervalBetweenAttacks)
                        {
                            if (isSkullSoldier || (isEye && isTransformed) || isSkull)
                            {
                                animator.SetBool("attack", false);
                            }
                            chargingTime = 0f;
                            if (isBoss)
                            {
                                castingTime = castingTime + Time.deltaTime;
                            }
                        }
                        else
                        {
                            if (isSkullSoldier || (isEye && isTransformed) || isSkull)
                            {
                                animator.SetBool("attack", false);
                            }
                            if (isBoss)
                            {
                                castingTime = castingTime + Time.deltaTime;
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
                        randomAction(2);
                        move();
                    }
                }
            }
        }
    }
        //Debug.Log(susLevel);
        
    void Die(){
            if (isLargePumpkin)
            {
                for (int i = 0; i < enemyLevel * 2; i++)
                {
                    GameObject smallPumpkinClone = Instantiate(smallPumpkin, transform.position, transform.rotation);
                    // Small pumpkins are weaker, but their attack speed is faster
                    smallPumpkinClone.GetComponent<EnemyBehavior>().ally = ally;
                    smallPumpkinClone.GetComponent<EnemyBehavior>().enemyLevel = enemyLevel;
                    smallPumpkinClone.GetComponent<EnemyBehavior>().maxAttackRange = maxAttackRange * 0.75f;
                    smallPumpkinClone.GetComponent<EnemyBehavior>().health = (int)Mathf.Round(oldHealth / 2f);
                    smallPumpkinClone.GetComponent<EnemyBehavior>().intervalBetweenAttacks = intervalBetweenAttacks / 1.5f;
                }
                
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
    }
}
