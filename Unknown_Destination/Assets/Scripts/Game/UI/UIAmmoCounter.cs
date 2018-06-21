//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//01 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoCounter : MonoBehaviour {

    private player_Manager playerManager;

    public Text ammoCount;

    // Use this for initialization
    void Start () {
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<player_Manager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerManager.curAmmo < 100)
        {
            ammoCount.color = new Color(255f, 0f, 0f, 255f);
        }
        else
        {
            ammoCount.color = new Color(255f, 255f, 255f, 255f);
        }
        ammoCount.text = "" + playerManager.curAmmo;
	}
}
