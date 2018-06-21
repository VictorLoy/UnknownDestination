//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//07 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {

    public GameObject bullet;
    public float Bulletvelocity = 1000;
    public float fireRate = 5;
    private simpleEnemyAI enemy;

    private float timeToFire = 2;

    // Use this for initialization
    void Start () {
		enemy = gameObject.GetComponentInParent<simpleEnemyAI>();
    }
	
	// Update is called once per frame
	void Update () {
        StartShooting();
    }

    public void StartShooting()
    {
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity);
        projectile.transform.rotation = gameObject.transform.rotation;
        //Choose direction to shoot in
        if (enemy.facingRight)
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Bulletvelocity * Time.deltaTime, 0);
        else
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Bulletvelocity * Time.deltaTime * -1, 0);
    }
}
