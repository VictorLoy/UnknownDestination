using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemybar : MonoBehaviour {

    public Slider bar;

    public Transform target;
    public Vector3 offset = new Vector3(0, 1, 0);
   

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset );
        }
    }
}