using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slimepurple_healthbar : MonoBehaviour {

	public PurpleSlimeAI enemyScript;
	private float enemyHealth;
	private float maxHealth;
	public Image healthBar;

	// Use this for initialization
	void Start()
	{
		maxHealth = enemyScript.maxHealth;
		healthBar = gameObject.GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		enemyHealth = enemyScript.health;
		//Show if health is not 100%
		if (enemyHealth != maxHealth)
		{
			gameObject.GetComponentInParent<Canvas>().enabled = true;
		}
		else
		{
			gameObject.GetComponentInParent<Canvas>().enabled = false;
		}

		//Have the fill of the bar match the enemies health


		healthBar.fillAmount = (enemyHealth) / maxHealth;
	}
}
