using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameRules : MonoBehaviour
{

    public GameObject gameRules;
    public GameObject pauseMenu;


    void Start()
    {
        gameRules.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void getBack(){
        gameRules.SetActive(false);
        pauseMenu.SetActive(true);
    }
}

