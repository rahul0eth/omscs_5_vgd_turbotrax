using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeCreditsMenu : MonoBehaviour
{
    public GameObject credits;
    public GameObject pauseMenu;


    void Start()
    {
        credits.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void getBack(){
        credits.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
