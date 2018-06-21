//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//02 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletKiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(timedDeath());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platforms" || collision.gameObject.tag == "walls" || collision.gameObject.tag == "obstacle")
        {
            if (gameObject != null)
                Destroy(gameObject);
        }
    }

    IEnumerator timedDeath()
    {
        yield return new WaitForSeconds(2);
        if(gameObject != null)
            Destroy(gameObject);
    }
}
