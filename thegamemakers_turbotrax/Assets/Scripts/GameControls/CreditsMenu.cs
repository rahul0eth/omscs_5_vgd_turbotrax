using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject credits;


    void Start()
    {
        credits.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void getBack(){
        credits.SetActive(false);
        mainmenu.SetActive(true);
    }
}
