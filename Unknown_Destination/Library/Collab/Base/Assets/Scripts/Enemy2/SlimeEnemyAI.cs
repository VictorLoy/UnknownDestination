using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyAI : MonoBehaviour
{


	public float enemySpeed = 1.0f;
	public float leftPoint = 0.0f;
	public float rightPoint = 5.0f;
	public float detectingRange = 5.0f;
	float walkingDirection = 1.0f;
	public float distanceToPlayer;
	public float displacementToplayer;
	public Transform player;
	Vector2 walkAmount;
	bool facingRight = false;
	float originalX; // Original float value
	public float health= 4;
	public float bulletDamage = 1;


	void Start()
	{
        this.originalX = this.transform.position.x;	
		leftPoint = transform.position.x - leftPoint;
		rightPoint= transform.position.x + rightPoint;
		player = GameObject.Find("Hero").transform;
		if (player == null) Debug.Log("Player not found");

	}
	void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;	
	
	}

	 void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "bullets")
		{
			health = health - 1;
			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
		if (collision.gameObject.tag == "hero")
		{
			//Damage Player
		}

	}


	// Update is called once per frame
	void Update()
	{
	walkAmount.x = walkingDirection* enemySpeed * Time.deltaTime;
		if (walkingDirection > 0.0f && transform.position.x >= rightPoint) {
			walkingDirection = -1.0f;
			Flip();
		} else if (walkingDirection< 0.0f && transform.position.x <= leftPoint) {
			walkingDirection = 1.0f;
			Flip();
		}
		transform.Translate(walkAmount);
		distanceToPlayer = Vector3.Distance ((Vector2)transform.position, (Vector2)player.position);
		displacementToplayer = player.position.x - transform.localPosition.x;

	}


}
