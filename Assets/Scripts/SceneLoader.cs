﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
public void ReloadGame()
    {
        Debug.Log("lets play again");
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void theyWon()
    {
        FindObjectOfType<AudioController>().Play("gameWin");
    }
}
