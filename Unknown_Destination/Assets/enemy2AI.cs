using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2AI : MonoBehaviour {

	public float speed = 1.0f; 
	public float detectingRange = 5.0f;
	public float maxHP = 80f;
	public float health;
	public float slimeDamage;
	public float yAxisDistance;
	public float yAxisDetectRange;
	public float distanceToPlayer;
	public float displacementToPlayer;
	public Transform player;
	private float z = 0f;
	private Rigidbody2D rb2d;
	public float direction;
	public float playerDist;


	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
	}
	
	// Update is called once per frame
	void fixedUpdate () {
		distanceToPlayer = Vector3.Distance ((Vector2)transform.position, (Vector2)player.position);
		yAxisDistance = Mathf.Abs (transform.position.y - player.position.y);
		displacementToPlayer = player.position.x - transform.position.x;
		chase ();
		gameObject.transform.eulerAngles = new Vector3(0, 0, direction);
		if (speed == 0)
			rb2d.velocity = new Vector2(0f, 0f);
		else
			rb2d.AddForce(gameObject.transform.up * speed);
	}

	void chase(){
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
	}
}
