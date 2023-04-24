using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{


    public GameObject mainmenu;
    public GameObject gameRules;


    void Start()
    {
        gameRules.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void getBack(){
        gameRules.SetActive(false);
        mainmenu.SetActive(true);
    }
}
