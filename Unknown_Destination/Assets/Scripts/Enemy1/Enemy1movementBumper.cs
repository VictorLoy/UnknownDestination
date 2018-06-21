//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//06 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1movementBumper : MonoBehaviour {

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
        if (collision.gameObject.tag == "walls" || collision.gameObject.tag == "platforms" || collision.gameObject.tag == "obstacle")
        {
            parentScript.Movementbumper();
        }
    }
}
