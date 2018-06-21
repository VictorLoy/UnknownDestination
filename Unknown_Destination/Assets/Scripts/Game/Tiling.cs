//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//27 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{

    public int offsetX = 2;

    //Check if need to generate more
    public bool hasRight = false;
    public bool hasLeft = false;

    public bool reverseScale = false; //Fixes graphical errors, looks more seamless

    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;
    private float xScale;
    public Transform parent;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
        //xScale = myTransform.localScale.x;
    }

    // Use this for initialization
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update() {

        //If we need to generate more
        if (!hasLeft || !hasRight)
        {
            //Calculate the camera';s visible distance relative to the world coordinates
            float camHorizontalReach = cam.orthographicSize * Screen.width / Screen.width;

            //Calculate x where cam can see the edge of the sprite
            float edgeVisiblePosRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalReach;
            float edgeVisiblePosLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalReach;

            //Check if we can see the edge of the sprite, if we can make new
            if (cam.transform.position.x >= edgeVisiblePosRight - offsetX && !hasRight)
            {
                MakeNewGrowth(1);
                hasRight = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePosLeft + offsetX && !hasLeft)
            {
                MakeNewGrowth(-1);
                hasLeft = true;
            }

        }
    }

    void MakeNewGrowth(int rightOrLeft)
    {
        //Calculate new position for growth
        Vector3 newPosition = new Vector3(myTransform.position.x + myTransform.localScale.x * spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newGrowth = (Transform)Instantiate(myTransform, newPosition, myTransform.rotation);

        //Allows for backgrounds to be seamlessly created
        if (reverseScale == true)
        {
            newGrowth.localScale = new Vector3(newGrowth.localScale.x * (-1), newGrowth.localScale.y, newGrowth.localScale.z);
        }

        //ensure it is kept clean in the inspector
        newGrowth.transform.parent = parent.transform;

        //Tell the growth that one exists before or after it
        if (rightOrLeft > 0)
        {
            newGrowth.GetComponent<Tiling>().hasLeft = true;
        }
        else
        {
            newGrowth.GetComponent<Tiling>().hasRight = true;
        }
    }
}