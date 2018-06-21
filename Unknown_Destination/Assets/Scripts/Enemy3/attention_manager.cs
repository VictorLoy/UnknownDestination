using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attention_manager : MonoBehaviour {
    public Enemy3Shoot at;
	private SpriteRenderer sprite;
	public GameObject attention;
	// Use this for initialization
	void Start () {
		sprite = attention.gameObject.GetComponent<SpriteRenderer>();
		at = attention.gameObject.GetComponentInParent<Enemy3Shoot>();
	}
	
	// Update is called once per frame
	void Update () {
		if (at.shooting) {
			sprite.sortingLayerName = "items";
		}
		else
			sprite.sortingLayerName = "Backdrop";
	}
}
