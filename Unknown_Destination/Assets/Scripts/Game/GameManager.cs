//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//02 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //If esc is hit return to the main menu
        if (Input.GetKey("escape"))
            SceneManager.LoadScene(0);
    }
}
