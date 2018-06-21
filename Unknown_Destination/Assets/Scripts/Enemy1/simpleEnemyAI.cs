//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//02 Nov 2017


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemyAI : MonoBehaviour {

    //Components
    Rigidbody2D enemyRB;
    public Transform player;
    GameObject shootPoint1;
    public LayerMask toDetect;
    private player_Manager HeroManager;
    Vector3[] path;

    //Integers
    int targetIndex;

    //Floats
    public float enemySpeed = 1f;
    private float timeInDirection;
    private float distanceFromPlayer;
    private float distanceFromPlayerYaxis;
    public float engageSpeed = 2f;
    public float virticalChaseSpeed = 1f;
    public float health;
    public float maxHealth = 100;
    public float bulletDamage = 3;
    [HideInInspector] public float effectiveShootDistance = 30f;
    [HideInInspector] public float maxChaseDistance = 40f;

    //Booleans
    [HideInInspector] public bool detected = false;
    [HideInInspector] public bool facingPlayer;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool movingRight = true;
    [HideInInspector] public bool inFrontOfPlayer;
    [HideInInspector] public bool playerAbove;
    private bool canShoot = false;
    [HideInInspector] public bool canMoveUp = true;
    [HideInInspector] public bool canMoveDown = true;
    public bool isObstacle;
    public bool playerIsDead = false;
    public bool DrawPath = false;
    private bool ObstacleUp;
    private bool ObstacleDown;

    //Manage coroutines
    bool PatrolSwitcherRunning;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        StartCoroutine(PatrolSwitcher());
        PatrolSwitcherRunning = true;
        player = GameObject.FindGameObjectWithTag("player").transform;
        shootPoint1 = GameObject.Find("shootPoint1");
        shootPoint1.SetActive(false);
        StartCoroutine(ShootSelector());
        health = maxHealth;
        HeroManager = player.gameObject.GetComponent<player_Manager>();
    }

    public void Update()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);
        distanceFromPlayerYaxis = player.position.y - transform.position.y;

        //Set dynamic booleans
        if(player.position.x - transform.position.x > 0)
        {
            inFrontOfPlayer = false;
        }
        else
        {
            inFrontOfPlayer = true;
        }
        if(distanceFromPlayerYaxis > 0)
        {
            playerAbove = true;
        }
        else
        {
            playerAbove = false;
        }

        //Know players state
        playerIsDead = HeroManager.isDead;

        //Find if the enemy is facing the player
        if (!inFrontOfPlayer && facingRight)
        {
            facingPlayer = true;
        }
        else if (inFrontOfPlayer && !facingRight)
        {
            facingPlayer = true;
        }
        else facingPlayer = false;

        //If player is within vision on the enemy
        if (facingPlayer && distanceFromPlayer < 10 && distanceFromPlayerYaxis < 2 && !isObstacle && !playerIsDead)
        {
            detected = true;
        }

        if (distanceFromPlayer > maxChaseDistance)
        {
            detected = false;
        }

        if(playerIsDead)
        {
            Patrol();
            detected = false;
            canShoot = false;
            shootPoint1.SetActive(false);
        }

        //Deal with enemy death
        if(health < 0)
        {
            Dead();
        }
    }

    private void FixedUpdate()
    {
        //To avoid errors if player does not exist
        if(player == null)
        {
            Patrol();
        }

        //Manage detection
        if (!detected)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
            if (!PatrolSwitcherRunning)
            {
                StartCoroutine(PatrolSwitcher());
                PatrolSwitcherRunning = true;
            }
            Patrol();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);
            StopCoroutine(PatrolSwitcher());
            PatrolSwitcherRunning = false;
            Engage();
        }
        //If there are obstacles in the way of the enemy and hero
        CheckObstacle();
    }

    //Manage health if hit with bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullets")
        {
            if(!detected)
            {
                detected = true;
            }
            Destroy(collision.gameObject);
            health -= bulletDamage;
        }

        if(collision.gameObject.tag == "player")
        {
            detected = true;
        }
    }

    //Manage cool down of weapon shooting
    IEnumerator ShootSelector()
    {
        while(true)
        {
            canShoot = !canShoot;
            yield return new WaitForSeconds(3);
        }
    }

    //Moves enemy left and right
    void Patrol()
    {
        if (movingRight)
            enemyRB.velocity = new Vector2(enemySpeed, 0);
        else
            enemyRB.velocity = new Vector2(-enemySpeed, 0);
    }

    //If child bumper gets a collision
    public void Movementbumper()
    {
        flipDirection();
    }

    //Helps the patrol method switch directions
    IEnumerator PatrolSwitcher()
    {
        while(!detected)
        {
            if(detected)
            {
                break;
            }
            timeInDirection = Random.Range(3, 10);
            flipDirection();
            yield return new WaitForSeconds(timeInDirection);
        }
        yield return null;
    }

    //Shoot at the hero
    void Engage()
    {
        KeepPlayerDistance();
        if(canShoot && !playerIsDead && Mathf.Abs(distanceFromPlayerYaxis) < 2) // Added the distance from y axis so that the enemy will not shoot at nothing
        {
            shootPoint1.SetActive(true);
        }
        if(!canShoot)
        {
            shootPoint1.SetActive(false);
        }
    }

    //Tracks distances from the player and will hold certain threshholds
    void KeepPlayerDistance()
    {
        if(isObstacle)
        {
            PathRequestManager.RequestPath(transform.position, player.position, OnPathFound);
        }
        else
        {
            if (!facingPlayer)
            {
                flipDirection();
            }
            //Maintain a certain x distance from the player
            if (distanceFromPlayer > 8 && !inFrontOfPlayer && !isObstacle)
            {
                enemyRB.velocity = new Vector2(engageSpeed, enemyRB.velocity.y);
            }
            if (distanceFromPlayer > 8 && inFrontOfPlayer && !isObstacle)
            {
                enemyRB.velocity = new Vector2(-engageSpeed, enemyRB.velocity.y);
            }
            if(distanceFromPlayer > 8)
            {
                PathRequestManager.RequestPath(transform.position, player.position, OnPathFound);
            }

            //Maintain same y as player
            if (distanceFromPlayerYaxis > 0.5 && playerAbove && canMoveUp && !ObstacleUp)
            {
                enemyRB.velocity = new Vector2(enemyRB.velocity.x, virticalChaseSpeed);
            }
            else if (distanceFromPlayerYaxis > 0.5 && ObstacleUp)
            {
                PathRequestManager.RequestPath(transform.position, player.position, OnPathFound);
            }
            if (distanceFromPlayerYaxis < -0.5 && !playerAbove && canMoveDown && !ObstacleDown)
            {
                enemyRB.velocity = new Vector2(enemyRB.velocity.x, -virticalChaseSpeed);
            }
            else if (distanceFromPlayerYaxis < -0.5 && ObstacleDown)
            {
                PathRequestManager.RequestPath(transform.position, player.position, OnPathFound);
            }
        }

    }

    //This enables pathfinding on the enemy
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, enemySpeed * 2 * Time.deltaTime);
            yield return null;
        }
    }

    //Flips which way the enemy is facing
    void flipDirection()
    {
        movingRight = !movingRight;
        facingRight = !facingRight;
        Vector3 theScale = gameObject.transform.localScale;
        theScale.x *= -1;
        gameObject.transform.localScale = theScale;
    }

    //Casts a RayCast2D detecting what is within 10 units of its position.
    //This is used to detect obstacles and help with the hero being able to hide
    public void CheckObstacle()
    {
        Vector2 direction;
        //Which way to cast
        if (facingRight)
        {
            direction = new Vector2(1, 0);
        }
        else
        {
            direction = new Vector2(-1, 0);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10, toDetect);
        //If not detecting an obstacle then there is none
        if(null == hit.collider)
        {
            isObstacle = false;
        }
        else if(hit.collider.gameObject.tag == "obstacle")
        {
            //If player is in front of the obstacle then he is detectable
            if(Vector2.Distance(transform.position, hit.collider.gameObject.transform.position) > distanceFromPlayer)
            {
                isObstacle = false;
            }
            else
            {
                isObstacle = true;
            }
        }
    }

    //Casts a RayCast2D detecting what is within 2 units up and down of the enemy.
    //This is used to detect obstacles and help with using pathfinding or not
    public void CheckObstacleUPandDown()
    {
        Vector2 up = new Vector2(0, 1);
        Vector2 down = new Vector2(0, -1);

        RaycastHit2D checkUp = Physics2D.Raycast(transform.position, up, 2, toDetect);
        RaycastHit2D checkDown = Physics2D.Raycast(transform.position, down, 2, toDetect);
        //If not detecting an obstacle then there is none
        if (null == checkUp.collider)
        {
            ObstacleUp = false;
        }
        if (null == checkDown.collider)
        {
            ObstacleDown = false;
        }
        else if (checkUp.collider.gameObject.tag == "obstacle")
        {
            ObstacleUp = true;
        }
        else if (checkDown.collider.gameObject.tag == "obstacle")
        {
            ObstacleDown = true;
        }
    }

    //Manage death routine
    void Dead()
    {
        gameObject.SetActive(false);
    }

    //This is to help visualize and debug paths of the enemy
    public void OnDrawGizmos()
    {
        if(DrawPath)
        {
            if(path != null)
            {
                for(int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if(i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }
    }
}
