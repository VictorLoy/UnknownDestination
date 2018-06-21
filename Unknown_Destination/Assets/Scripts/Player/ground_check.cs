//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//02 Nov 2017


//Script now obselete and not used

/*
 * This script manages whether or not a game object is standing on something or not
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_check : MonoBehaviour {

    private player_Movement player;

	// Use this for initialization
	void Start () {
        player = gameObject.GetComponentInParent<player_Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platforms")
        {
            player.grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platforms")
        {
            player.grounded = false;
        }
    }
}
