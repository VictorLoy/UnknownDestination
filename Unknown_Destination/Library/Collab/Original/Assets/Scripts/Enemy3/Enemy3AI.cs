jusing UnityEngine;
using System.Collections;


using UnityEngine;
using System.Collections;

public class Enemy3AI : MonoBehaviour {
	//Stats
	public float enemySpeed = 1.0f;     
	public float leftPoint = 0.0f;       
	public float rightPoint = 5.0f;    
	public float detectingRange = 5.0f;
	float walkingDirection = 1.0f;
	public float fireRange = 10f;
	public float maxHP = 200f;
	public float health;
	public float bulletDamage = 3f;
	public float yAxisDistance;
	public float yAxisDetectRange;
	public float distanceToPlayer;
	public float displacementToPlayer;
	Vector2 walkAmount;
	public bool facingLeft = false;
	public bool facingPlayer;
	float originalX; // Original float value
	//transform
	public Transform player;

	void Start () {
		player = GameObject.Find("Hero").transform;
		this.originalX = this.transform.position.x;	
		leftPoint = transform.position.x - leftPoint;
		rightPoint= transform.position.x + rightPoint;
		health = maxHP;
	}

	void Flip() {
		facingLeft = !facingLeft;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FixedUpdate(){
	}
	// Update is called once per frame
	void Update () {
		walkAmount.x = walkingDirection * enemySpeed * Time.deltaTime;
		if (walkingDirection > 0.0f && transform.position.x >= rightPoint) {
			walkingDirection = -1.0f;
			Flip ();
		} else if (walkingDirection < 0.0f && transform.position.x <= leftPoint) {
			walkingDirection = 1.0f;
			Flip ();
		}
		transform.Translate(walkAmount);
		distanceToPlayer = Vector3.Distance ((Vector2)transform.position, (Vector2)player.position);
		yAxisDistance = Mathf.Abs (transform.position.y - player.position.y);
		displacementToPlayer = player.position.x - transform.position.x;
//		facingCheck ();
		statusCheck();
		if (health <= 0) {
			gameObject.SetActive (false);
		}
	}

//	void facingCheck(){
//
//		if (transform.localScale.x < 0) {
//			facingRight = true;
//			Debug.Log (transform.localScale.x);
//			Debug.Log ("c");
//		} else {
//			facingRight = false;
//		}
//	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "bullets")
		{
            Destroy(collision.gameObject);
			health -= bulletDamage;
		}
	}
	void statusCheck(){
		if ((facingLeft && displacementToPlayer < 0) || (!facingLeft && displacementToPlayer > 0))
		{
			facingPlayer = true;
		} else {
			facingPlayer = false;
		}
	}
}