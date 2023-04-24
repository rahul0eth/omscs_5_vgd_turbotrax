using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseScript : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    public GameObject carDashboard;

    public GameObject racingCar;
    private CarDriveV2 carDriveV2;

    // public GameObject pauseMenu;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("Doesn’t find the component you are looking for.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        carDriveV2 = racingCar.GetComponent<CarDriveV2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!carDriveV2.getIsPausedForNarration())
        {
            if (Input.GetButtonDown("Escape"))
            {
                if (canvasGroup.interactable)
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.alpha = 0f;
                    Time.timeScale = 1f;
                    carDashboard.SetActive(true);
                }
                else
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;
                    Time.timeScale = 0f;
                    carDashboard.SetActive(false);
                }
            }
        }
    }
}
