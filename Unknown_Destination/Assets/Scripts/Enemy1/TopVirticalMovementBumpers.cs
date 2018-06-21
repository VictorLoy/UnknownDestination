//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//27 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script detects where a bumper on the top of the player has collided or not allowing the player to continue moving up or not
 */

public class TopVirticalMovementBumpers : MonoBehaviour {

    private simpleEnemyAI parentScript;

    // Use this for initialization
    void Start () {
        parentScript = gameObject.GetComponentInParent<simpleEnemyAI>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "walls" || collision.gameObject.tag == "platforms")
        {
            parentScript.canMoveUp = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "walls" || collision.gameObject.tag == "platforms")
        {
            parentScript.canMoveUp = true;
        }
    }
}
