using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainmenu;

    public Slider slider;
    public delegate void MyVariableChanged(float newValue);
    public static event MyVariableChanged sliderVariableChanged;

    private string inputDirections = "WASD";
    private string previousDirections;
    public delegate void EmitDirections(string newValue);
    public static event EmitDirections directionsChanged;


    void Start()
    {
        settingsMenu.SetActive(true);
        mainmenu.SetActive(false);


        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void getBack(){
        settingsMenu.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void OnSliderValueChanged(float value){
        // This will be called whenever the slider value changes
        Debug.Log("Slider value changed to: " + value);

        // Emitting the slidder response
        if (value != slider.value){
            slider.value = value;
            sliderVariableChanged?.Invoke(slider.value);
        }
    }

    public void switchToWASDDirections(bool selectedButton){
        if (selectedButton){
            inputDirections = "WASD";
        }
    }

    public void switchToArrowsDirections(bool selectedButton){
        if (selectedButton){
            inputDirections = "Arrows";
        }
    }

    void update(){

        // Emitting the directions selection
        if(previousDirections != inputDirections){
            previousDirections = inputDirections;
            directionsChanged?.Invoke(inputDirections);
        }
    }


}
