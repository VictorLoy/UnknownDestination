using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour {

    private spawnManager spawner;

    public bool playerInSpawn = false;

    // Use this for initialization
    void Start () {
        spawner = GameObject.Find("_GameManager").GetComponent<spawnManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            spawner.currentSpawn = gameObject.transform;
            playerInSpawn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            playerInSpawn = false;
        }
    }
}
