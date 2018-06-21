using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersound : MonoBehaviour
{
	private player_Manager player_script;
    // Use this for initialization
    void Start()
    {
		player_script = gameObject.GetComponent<player_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButton("Fire1") && player_script.curAmmo>0)
        {
            AudioSource shoot = GetComponent<AudioSource>();
            shoot.Play();
        }
    }
}
