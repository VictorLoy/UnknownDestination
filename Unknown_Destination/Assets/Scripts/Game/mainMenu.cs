//Created by Marcus Hamilton for Team Unknown
//11173915
//mah985
//02 Nov 2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

    public void StartGame(string startGame)
    {
        SceneManager.LoadScene(1);
    }

    public void Quit(string Quit)
    {
        Application.Quit();
    }


}
