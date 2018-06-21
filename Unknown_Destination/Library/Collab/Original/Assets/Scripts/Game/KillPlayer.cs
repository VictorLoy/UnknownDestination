using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
	public LevelManager levelMan;
	// Use this for initialization
	void Start()
	{
		levelMan = FindObjectOfType<LevelManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	void onTriggerEnter2D(Collider2D other)//This function automatically kills the player when he touches the platform
	{
		if (other.tag == "player")
		{
			levelMan.respawnPlayer();
		}

	}
}
