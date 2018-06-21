//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//02 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullets : MonoBehaviour {

    public GameObject bullet;
    private player_Manager playerManager;
    public float Bulletvelocity;
    public float fireRate = 50;

    private float timeToFire = 0;

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponentInParent<Animator>();
        playerManager = gameObject.GetComponentInParent<player_Manager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButton("Fire1") && Time.time > timeToFire && playerManager.canShoot)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
            playerManager.Shooting();
        }
	}

    void Shoot()
    {
        GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);
        projectile.transform.rotation = gameObject.transform.rotation;
        //Choose direction to shoot in
        if(player_Movement.facingRight)
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Bulletvelocity * Time.deltaTime, 0);
        else
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Bulletvelocity * Time.deltaTime * -1, 0);
        anim.SetBool("shoot", true);
    }

}
