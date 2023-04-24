using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject gameRules;
    public GameObject credits;
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        gameRules.SetActive(false);
        credits.SetActive(false);
    }

    public void switchToSettings(){
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void switchToGameRules(){
        pauseMenu.SetActive(false);
        gameRules.SetActive(true);
    }

    public void SwitchToCredits(){
        pauseMenu.SetActive(false);
        credits.SetActive(true);
    }
}
