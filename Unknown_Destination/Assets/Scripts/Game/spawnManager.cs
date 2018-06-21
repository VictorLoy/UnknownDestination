//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//01 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{

    public player_Manager playerManager;

    public Transform currentSpawn;
    public Transform toSpawn;

    // Use this for initialization
    void Start()
    {
        currentSpawn = GameObject.FindGameObjectWithTag("startSpawn").transform;
        toSpawn = GameObject.FindGameObjectWithTag("player").transform;
        playerManager = GameObject.FindGameObjectWithTag("player").GetComponent<player_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SimulateSpawn()
    {
        Vector3 gotoPosition = new Vector3(currentSpawn.position.x, currentSpawn.position.y, toSpawn.position.z);
        toSpawn.position = gotoPosition;
        playerManager.curHealth = playerManager.maxHealth / 2;
        StartCoroutine(playerReset());
    }

    IEnumerator playerReset()
    {
        yield return new WaitForSeconds(1);
        playerManager.isDead = false;
        yield return null;
    }


}
