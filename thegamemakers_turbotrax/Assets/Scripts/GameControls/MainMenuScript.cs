using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{

    public GameObject settingsMenu;
    public GameObject mainmenu;
    public GameObject gameRules;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        mainmenu.SetActive(true);
        settingsMenu.SetActive(false);
        gameRules.SetActive(false);
        credits.SetActive(false);
    }

    public void switchToSettings(){
        mainmenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void switchToGameRules(){
        mainmenu.SetActive(false);
        gameRules.SetActive(true);
    }

    public void SwitchToCredits(){
        mainmenu.SetActive(false);
        credits.SetActive(true);
    }
}
