//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//04 Oct 2017


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_Movement : MonoBehaviour {

    //Components
    Rigidbody2D r_body;
    Animator anim;

    //Floats
    public float defaultJumpHeight = 500f;
    public float jumpHeight;
    public float maxSpeed = 10f;
    public float speed = 50f;
    public float AirSpeed = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 3f;
    [HideInInspector] public static float CameraChaseSpeed;

    //Bools
    public static bool facingRight;
    public bool grounded;
    public bool canDoubleJump;

    // Use this for initialization
    void Start()
    {
        r_body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpHeight = defaultJumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
		if (gameObject.transform.localScale.x > 0) {
			facingRight = true;
		} else {
			facingRight = false;
		}
        checkGrounded();
        //Improve jump proformance
        if(r_body.velocity.y < 0)
        {
            r_body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (r_body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            r_body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // get left/right, and virtical forces
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", Mathf.Abs(h));
        anim.SetBool("onGround", grounded);

        //This helps the camera know its chase speed to make it feel
        //a bit more dynamic.
        CameraChaseSpeed = (Mathf.Abs(r_body.velocity.x) - 1);

        if(grounded)
        {
            r_body.drag = 7; //Using drag to avoid sliding on the ground.
            groundedMovement(h);
        }
        
        if(!grounded)
        {
            r_body.drag = 0; //Drag to 0 so that player falls naturally
            airMovement(h);
        }

        if (Input.GetButtonDown("Jump"))
        {
            //If on the ground you can jump
            if(grounded)
            {
                r_body.AddForce(Vector2.up * jumpHeight);
                canDoubleJump = true; //Ability to jump twice
            }
            else //In the air
            {
                if(canDoubleJump) //havn't already double jumped
                {
                    canDoubleJump = false;
                    r_body.velocity = new Vector2(r_body.velocity.x, 0);
                    r_body.AddForce(Vector2.up * jumpHeight/1.5f); //Second jump will be slightly less
                }
            }
        }

        if ((h < 0 && facingRight) || (h > 0 && !facingRight))
        {
            Flip();
        }

    }

    private void groundedMovement(float h)
    {
        // add forces for movement
        r_body.AddForce(new Vector2(h * speed, 0));

        //Limit speed to a max moving right
        if (r_body.velocity.x > maxSpeed)
        {
            r_body.velocity = new Vector2(maxSpeed, r_body.velocity.y);
        }

        //Limit speed to a max moving left
        if (r_body.velocity.x < -maxSpeed)
        {
            r_body.velocity = new Vector2(-maxSpeed, r_body.velocity.y);
        }
    }

    private void airMovement(float h)
    {

        // add forces for movement
        r_body.AddForce(new Vector2(h * AirSpeed, 0));

        //Limit speed to a max moving right
        if (r_body.velocity.x > maxSpeed)
        {
            r_body.velocity = new Vector2(maxSpeed, r_body.velocity.y);
        }

        //Limit speed to a max moving left
        if (r_body.velocity.x < -maxSpeed)
        {
            r_body.velocity = new Vector2(-maxSpeed, r_body.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void checkGrounded()
    {
        bool isIn = false;
        Collider2D[] inContact = new Collider2D[10];
        r_body.GetContacts(inContact);
        for(int i = 0; i < inContact.Length; i++)
        {
            if(null == inContact[i])
            {

            }
            else
            {
                if(inContact[i].gameObject.tag == "platforms" || inContact[i].gameObject.tag == "enemy")
                {
                    isIn = true;
                }
            }
        }
        if (isIn)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
