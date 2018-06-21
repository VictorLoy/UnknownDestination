//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//01 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Manages all stats and abilities of the hero
*/

public class player_Manager : MonoBehaviour {

    //Components
    Rigidbody2D r_body;
    private player_Movement playerMovement;
    private spawnManager spawner;
    public GameObject explosion;


    //Floats
    float invincibilityTime = 3f;

    //Booleans
    public bool isDead = false;

    //Stats
    public float curHealth;
    public float maxHealth = 100;
    public float curAmmo;
    public float maxAmmo = 100;
    public float megaJumpHeight = 800f;

    //Abilities
    public bool isInvincible = false;
    public bool fastRun = false;
    public bool canMegaJump = false;
    public bool canShoot = true;

    // Use this for initialization
    void Start () {
        curHealth = maxHealth;
        curAmmo = maxAmmo;
        playerMovement = gameObject.GetComponentInParent<player_Movement>();
        spawner = GameObject.Find("_GameManager").GetComponent<spawnManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //Monitor MegaJump values
        MegaJump();
        //Manage Health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth < 0)
        {
            isDead = true;
            Die();
        }

        //Check ammo supplies
        if(curAmmo <= 0)
        {
            canShoot = false;
            curAmmo = 0f;
        }
        else
        {
            canShoot = true;
        }
        if(curAmmo > maxAmmo)
        {
            curAmmo = maxAmmo;
        }
    }

    //Manage all triggers on player for abilities and stats related items
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Manage Hits
        if(!isInvincible)
        {
            if (collision.gameObject.tag == "enemyBullet")
            {
                Destroy(collision.gameObject);
                curHealth -= 2f;
            }
            if (collision.gameObject.tag == "enemyLaser")
            {
                Destroy(collision.gameObject);
                curHealth -= 20;
            }
            if (collision.gameObject.tag == "enemyExplosives")
            {
                Destroy(collision.gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
                curHealth -= 10;
            }
			if (collision.gameObject.tag == "saw") {
				curHealth -= 20;
			}
			if (collision.gameObject.tag == "spikes") {
				curHealth -= 101;
			}
			if (collision.gameObject.tag == "Respawn") {
				curHealth -= 50;
			}
        }
        if (isInvincible)
        {
            if (collision.gameObject.tag == "enemyBullet")
            {
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "enemyLaser")
            {
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "enemyExplosives")
            {
                Destroy(collision.gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }

            //Manage Pickups

            if (collision.gameObject.tag == "health")
        {
            Destroy(collision.gameObject);
            if (curHealth + 20f > maxHealth)
            {
                curHealth = maxHealth;
            }
            else
            {
                curHealth += 20f;
            }
        }
        if (collision.gameObject.tag == "invincibility")
        {
            Destroy(collision.gameObject);
            Invincibility();
        }
        if (collision.gameObject.tag == "megaJump")
        {
            Destroy(collision.gameObject);
            canMegaJump = true;
        }
        if (collision.gameObject.tag == "ammo")
        {
            Destroy(collision.gameObject);
            if (curAmmo + 100f > maxAmmo)
            {
                curAmmo = maxAmmo;
            }
            else
            {
                curAmmo += 100f;
            }
        }
    }

    //Manages ammo while shooting
    public void Shooting()
    {
        curAmmo -= 1;
        //Add sound effects here
    }

    //Manage invincibility and timeout
    public void Invincibility()
    {
        StartCoroutine(InvincibilityTimer());
    }

    IEnumerator InvincibilityTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
        yield return null;
    }

    public void MegaJump()
    {
        if (canMegaJump)
        {
            playerMovement.jumpHeight = megaJumpHeight;
        }
        else
        {
            playerMovement.jumpHeight = playerMovement.defaultJumpHeight;
        }
        if(Input.GetButtonUp("Jump")) //Only lasts for one jump
        {
            canMegaJump = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "spikes"){
            curHealth -= 2f;
        }
    }


    void Die()
    { 
        spawner.SimulateSpawn();
    }
}
