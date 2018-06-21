using UnityEngine;
using System.Collections;


using UnityEngine;
using System.Collections;

public class Enemy3Shoot: MonoBehaviour {

	public GameObject bullet;
	public float Bulletvelocity = 1000;
	public float fireRate = 5;
	private Enemy3AI enemy;
	public bool shooting;
	private float curSpeed;
	private float timeToFire = 2;
	public bool wasShooting;

	// Use this for initialization
	void Start () {
		shooting = false;
		wasShooting = false;
		enemy = gameObject.GetComponentInParent<Enemy3AI>();
		curSpeed = enemy.enemySpeed;
	}

	// Update is called once per frame
	void Update () {
		gameObject.SetActive (true);
		if (enemy.facingPlayer && (enemy.distanceToPlayer < enemy.fireRange) && (enemy.yAxisDistance < enemy.yAxisDetectRange) && (!enemy.isObstacle)) {
			shooting = true;
			enemy.enemySpeed = 0f;
			wasShooting = true;
			StartShooting ();
		} else {
			if (wasShooting == true) {
				InvokeRepeating ("lostTarget", 2, 1);
				wasShooting = false;
			}
			shooting = false;
		}
	}

	void  lostTarget(){
		enemy.enemySpeed = curSpeed;
		enemy.anim.SetBool ("shooting", false);
		CancelInvoke ("lostTarget");
	}

	public void StartShooting()
	{
		if (Time.time > timeToFire)
		{
			timeToFire = Time.time + 1 / fireRate;
			enemy.anim.SetBool ("shooting", true);
			Shoot();
		}
	}

	void Shoot()
	{
		GameObject projectile = Instantiate(bullet, transform.position, Quaternion.identity );
		projectile.transform.rotation = gameObject.transform.rotation;
		//Choose direction to shoot in
		if (enemy.facingPlayer && enemy.facingLeft) {
			
			Vector3 scale = projectile.transform.localScale;
			scale.x *= -1;
			projectile.transform.localScale = scale;
			projectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Bulletvelocity * Time.deltaTime * -1, 0);
		}
		else if (enemy.facingPlayer && !enemy.facingLeft)
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Bulletvelocity * Time.deltaTime , 0);
	}
}