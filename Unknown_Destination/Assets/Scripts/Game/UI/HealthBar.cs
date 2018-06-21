//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//27 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private player_Manager playerManager;
    private Animator anim;

    public Image healthBar;

    // Use this for initialization
    void Start () {
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<player_Manager>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("health", playerManager.curHealth);
        if(playerManager.isInvincible)
        {
            healthBar.color = new Color(0f, 200f, 255f, 255f);
        }
        else
        {
            healthBar.color = new Color(255f, 255f, 255f, 255f);
            healthBar.fillAmount = (playerManager.curHealth) / 100f;
        }
    }
}
