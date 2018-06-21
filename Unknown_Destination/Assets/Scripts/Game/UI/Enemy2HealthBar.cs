using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2HealthBar : MonoBehaviour {

    private SlimeEnemyAI enemyScript;
    private float enemyHealth;
    private float maxHealth;
    public Image healthBar;

    // Use this for initialization
    void Start()
    {
        enemyScript = gameObject.GetComponentInParent<SlimeEnemyAI>();
        healthBar = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealth = enemyScript.health;
        maxHealth = enemyScript.maxHealth;
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
        if (enemyScript.facingRight)
        {
            healthBar.fillOrigin = 0;
        }
        else
        {
            healthBar.fillOrigin = 1;
        }
        healthBar.fillAmount = (enemyHealth) / maxHealth;
    }
}
