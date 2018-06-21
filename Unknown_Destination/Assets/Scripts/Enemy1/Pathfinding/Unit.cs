//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//09 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public bool DrawPath = true;
    public Transform target;
    float speed = 0.2f;
    Vector3[] path;
    int targetIndex;

	// Use this for initialization
	void Start () {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
	}

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while(true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector3[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }
    }

    //This is to help visualize and debug paths of the enemy
    public void OnDrawGizmos()
    {
        if (DrawPath)
        {
            if (path != null)
            {
                for (int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }
    }
}
