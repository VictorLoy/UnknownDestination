//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//06 Dec 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialMenuClose : MonoBehaviour {


    public void clearMenu(string clearMenu)
    {
        Destroy(transform.parent.gameObject);
        Time.timeScale = 1;
    }
    
}
