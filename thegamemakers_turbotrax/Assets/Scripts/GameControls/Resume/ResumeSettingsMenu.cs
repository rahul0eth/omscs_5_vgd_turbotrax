using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResumeSettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    public delegate void MyVariableChanged(float newValue);

    // private string inputDirections = "WASD";
    // private string previousDirections;
    public delegate void EmitDirections(string newValue);


    void Start()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);

        // slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void getBack(){
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    // public void OnSliderValueChanged(float value){
    //     // This will be called whenever the slider value changes
    //     Debug.Log("Slider value changed to: " + value);

    //     // Emitting the slidder response
    //     if (value != slider.value){
    //         slider.value = value;
    //         sliderVariableChanged?.Invoke(slider.value);
    //     }
    // }

    // public void switchToWASDDirections(bool selectedButton){
    //     if (selectedButton){
    //         inputDirections = "WASD";
    //     }
    // }

    // public void switchToArrowsDirections(bool selectedButton){
    //     if (selectedButton){
    //         inputDirections = "Arrows";
    //     }
    // }

    void update(){

        // Emitting the directions selection
        // if(previousDirections != inputDirections){
        //     previousDirections = inputDirections;
        //     directionsChanged?.Invoke(inputDirections);
        // }
    }


}
