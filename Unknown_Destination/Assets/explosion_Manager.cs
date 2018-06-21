using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_Manager : MonoBehaviour {
	// Use this for initialization
	public GameObject explosion;

	void Start () {
		Invoke ("Die", 1f);
	}

	void Die(){
		Destroy (gameObject);
	}
}
