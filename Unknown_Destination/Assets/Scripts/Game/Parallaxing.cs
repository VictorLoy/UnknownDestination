//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//27 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;
    public float smoothing = 1f;
    private float[] parallaxScales;
    private Vector3 previousCamPosition;    //Camera position in previous frame

    private Transform mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        previousCamPosition = mainCamera.position;
        parallaxScales = new float[backgrounds.Length];

        //Assigning parallax scales
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * (-1);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //Movment of cam needs to be effected by the scale
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPosition.x - mainCamera.position.x) * parallaxScales[i];

            //Set a target position 
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //Create the target vector 3 to have the background move
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //Movement smoothing
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //Update previousCamPosition
        previousCamPosition = mainCamera.position;
    }
}
