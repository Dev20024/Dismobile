using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Game Time")]
    [SerializeField] FloatVariable GameHour;
    [SerializeField] FloatVariable GameMinutes;
    [SerializeField] BoolVariable PM;
    public bool isTimeMoving;
    [Header("Real Time")]
    [SerializeField] FloatVariable RealMinutes;
    [SerializeField] FloatVariable RealSeconds;
    public float timeConversionRate = 48;
    public float currentTime = 0f;
    private int currentHourIndex;
    [Header("Events")]
    [SerializeField] GameEvent DayOver;
    [Header("Time Table")]
    int[] timeTable = new int[] {12,1,2,3,4,5,6,7,8,9,10,11,12}; 

    private void Start() {
        GameHour.value = timeTable[currentHourIndex];
        isTimeMoving = true;
    }

    private void OnDisable() {
        currentHourIndex = 0;
        GameHour.value = timeTable[currentHourIndex];
        GameMinutes.value = 0f;
        RealMinutes.value = 0f;
        RealSeconds.value = 0f;
        PM.value = false;
    }

    void Update()
    {
        if (!isTimeMoving) {return;}

        currentTime += (Time.deltaTime * timeConversionRate);

        // Game Time
            // minutes management
            if (currentTime >= 60f) {
                currentTime = 0f;
                GameMinutes.value += 1;
            }
            // hours management
            if (GameMinutes.value >= 60f) {
                GameMinutes.value = 0f;
                currentHourIndex++;
                // checking AM or PM
                if (currentHourIndex == 0) {PM.value = false;}
                else {PM.value = true;}
                // checking if final hour
                if (currentHourIndex == timeTable.Length - 1) {
                    DayFinished();
                }
                GameHour.value = timeTable[currentHourIndex];
            }
        // Real Time
            // seconds management
            RealSeconds.value =  RealSeconds.value + Time.deltaTime;
            // minutes management
            if (RealSeconds.value >= 60f) {
                RealSeconds.value = 0f;
                RealMinutes.value += 1f;
            }
        
    }

    public void DayFinished() {
        Debug.Log("Hit Final Hour");
        isTimeMoving = false;
        DayOver?.Fire();
    }
}
