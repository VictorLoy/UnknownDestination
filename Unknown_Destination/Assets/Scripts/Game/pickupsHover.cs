//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//27 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsHover : MonoBehaviour {

    public float amplitude = 0.2f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start () {
        posOffset = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
