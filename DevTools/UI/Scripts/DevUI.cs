using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DevUI : MonoBehaviour
{
    [Header("Car Stats : Variables")]
    [SerializeField] FloatVariable temperature;
    [SerializeField] StationVariable currentStation;
    [SerializeField] BoolVariable windowsOpen;

    [Header("Car Stats : UI")]
    [SerializeField] private TextMeshProUGUI temperatureDisplay;
    [SerializeField] private TextMeshProUGUI stationDisplay;
    [SerializeField] private TextMeshProUGUI windowsDisplay;

    [Header("Game Stats : Variables")]
    [SerializeField] FloatVariable GameHours;
    [SerializeField] FloatVariable GameMinutes;
    [SerializeField] BoolVariable PM;
    private string PMString  = "AM";
    [SerializeField] FloatVariable RealMinutes;
    [SerializeField] FloatVariable RealSeconds;
    [Header("Game Stats : UI")]
    [SerializeField] private TextMeshProUGUI gameTimeDisplay;
    [SerializeField] private TextMeshProUGUI realTimeDisplay;

    // Update is called once per frame
    void Update()
    {
        // update UI

        // car stats
        temperatureDisplay.text = "Temp: " + temperature.value.ToString();
        stationDisplay.text = "Station: " + currentStation.value.ToString();
        windowsDisplay.text = "Windows Open: " + windowsOpen.value.ToString();

        // game stats
        PMString = PM.value ? PMString = "PM" : PMString = "AM";
        gameTimeDisplay.text = GameHours.value.ToString() + ":" + GameMinutes.value.ToString() + " " + PMString;
        realTimeDisplay.text = RealMinutes.value.ToString() + ":" + ((int) RealSeconds.value).ToString();

    }
}
