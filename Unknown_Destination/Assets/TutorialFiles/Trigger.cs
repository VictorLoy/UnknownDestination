//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//06 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            GameObject.Find(gameObject.name + "Canvas").GetComponent<Canvas>().enabled = true;
            GameObject.Find(gameObject.name + "Canvas").GetComponent<freeze>().enabled = true;
        }
    }
}
