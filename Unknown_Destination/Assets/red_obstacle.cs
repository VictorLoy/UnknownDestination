using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_obstacle : MonoBehaviour {

	public float maxHealth=100;
	public float health;
	public GameObject explosion;
    private player_Manager player_script;
    private Transform player;
    private Grid gridUpdater;


	// Use this for initialization
	void Start () {
		health = maxHealth;
        player = GameObject.FindGameObjectWithTag("player").transform;
        player_script = player.GetComponent<player_Manager>();
        gridUpdater = GameObject.Find("PathFinding").GetComponent<Grid>();
    }




	// Update is called once per frame
	void FixedUpdate () {
		if (health <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
			gameObject.SetActive (false);
            //Calls to update pathfinding grid for new map
            gridUpdater.CreateGrid();
            if (Vector2.Distance(player.position, transform.position) <= 5)
            {
                player_script.curHealth -= 10; // If the player is too close he will be damaged
            }
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "bullets" || collision.gameObject.tag == "enemyBullet")
		{
			Destroy(collision.gameObject);
			health -= 2; //Barrel health decrease with each bullet
		}
    }
}
