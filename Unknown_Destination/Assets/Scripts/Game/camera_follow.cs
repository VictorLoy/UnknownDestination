//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//02 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour {

    public GameObject target;
    private Vector3 targetPosition;
    public float chaseSpeed;
    public float virticalOffSet = 6;
    public float horizontalOffSet = 0;

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("player") != null)
            target = GameObject.FindGameObjectWithTag("player");
        else
            Debug.Log("Object cannot be found to chase");
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("player") != null)
            target = GameObject.FindGameObjectWithTag("player");
        else
            Debug.Log("Object cannot be found to chase");
        chaseSpeed = player_Movement.CameraChaseSpeed;
        if(chaseSpeed < 0)
        {
            chaseSpeed = 5;
        }
        targetPosition = new Vector3(target.transform.position.x + horizontalOffSet, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
	}
}
