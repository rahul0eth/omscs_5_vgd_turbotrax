using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ResumeScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject carDashboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void resumeGame(){
        if (canvasGroup.interactable) {
            canvasGroup.interactable = false; 
            canvasGroup.blocksRaycasts = false; 
            canvasGroup.alpha = 0f; 
            Time.timeScale = 1f;
            carDashboard.SetActive(true);
        } else { 
            canvasGroup.interactable = true; 
            canvasGroup.blocksRaycasts = true; 
            canvasGroup.alpha = 1f; 
            Time.timeScale = 0f;
        }
    }
}
