using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

[RequireComponent(typeof(AdvancedCarController))]
[RequireComponent(typeof(npcDetection))]
public class PlayerCar : MonoBehaviour
{
    // exterior attributes
    public Characteristic AccelerationPower;
    public Characteristic maxSpeed;
    public Characteristic breakingPower;
    public Characteristic durability;
    // car controller
    private AdvancedCarController carController;
    // interior attributes
    [Header("Temperature")]
    // player car attributes
    public FloatVariable Temperature;
    [Header("Temperature : constraints")]
    [SerializeField] private FloatVariable minTemp;
    [SerializeField] private FloatVariable maxTemp;
    // temperature gauge
    [SerializeField] Transform minMarker;
    [SerializeField] Transform maxMarker;
    [SerializeField] Transform tempMarker;
    [Header("Windows")]
    public BoolVariable WindowsOpen;
    [Header("Radio Station")]
    public StationVariable currentStation;
    private List<RadioStation> radioStations = new List<RadioStation>();
    [Header("Passenger")]
    public bool isPassenger;
    public Transform seat;
    [Header("Animators")]
    [SerializeField] Animator wheelAnimator;
    [SerializeField] Animator stationDialAnimator;
    [SerializeField] Animator windowsAnimator;
    Vector3 newPos;
    [Header("Trackers")]
    [SerializeField] float CurrentSpeed;
    private Vector3 previousPos;
    private npcDetection npcDetecter;
    // events

    // initialize values
    private void Awake() {
    
        // initializing ratio stations
        radioStations.Add(RadioStation.Station_69);
        radioStations.Add(RadioStation.Station_97);
        radioStations.Add(RadioStation.Station_102);
        radioStations.Add(RadioStation.Station_111);
        radioStations.Add(RadioStation.Station_128);

        currentStation.value = radioStations[1];

        newPos = tempMarker.localPosition;
    }

    // set taxi's characteristics
    private void Start() {
        // car controller
        carController = GetComponent<AdvancedCarController>();
        carController.accelPower = AccelerationPower.Stat;
        carController.maxSpeed = maxSpeed.Stat;
        carController.BreakingPower = breakingPower.Stat;
        // npc detection
        npcDetecter = GetComponent<npcDetection>();
    }


    private float tParam;
    private void FixedUpdate() {
       // temp marker
       if (tParam < 1) {
            tParam += Time.deltaTime * .3f;
       }
       tempMarker.localPosition = new Vector3(newPos.x, Mathf.Lerp( tempMarker.localPosition.y,newPos.y,tParam),newPos.z);
       // speed tracking
       CurrentSpeed = (transform.position - previousPos).magnitude / Time.deltaTime;
       previousPos = transform.position;
       
       if (isPassenger || Mathf.RoundToInt(CurrentSpeed) != 0) {return;}
       npcDetecter.checkTaxiSides();
    }

    // player car events
    private void OnEnable() {
        RadioStationDial.OnDial += AdjustRadio;
        TempDownButton.OnTempDown += TempDown;
        TempUpButton.OnTempUp += TempUp;
        WindowsButton.OnWindowsChange += adjustWindows;
    }

    private void OnDisable() {
        RadioStationDial.OnDial -= AdjustRadio;
        TempDownButton.OnTempDown -= TempDown;
        TempUpButton.OnTempUp -= TempUp;
        WindowsButton.OnWindowsChange -= adjustWindows;
    }


    // functionality

    // radio station settings
    public void AdjustRadio() {
        int currentIndex = radioStations.IndexOf(currentStation.value);
        
        // check for loop
        if (currentIndex == (radioStations.Count - 1)) {
            currentIndex = 0;
        }
        else {
            currentIndex++;
        }

       currentStation.value = radioStations[currentIndex];
       stationDialAnimator.SetTrigger("OnDial");
    }

    // adjust windows
    public void adjustWindows() {
        WindowsOpen.value = !WindowsOpen.value;
        windowsAnimator.SetBool("WindowsOpen",WindowsOpen.value);
    }

    // adjust temperature
    public void TempUp() {
        Temperature.value++;
        Temperature.value = Mathf.Clamp(Temperature.value, minTemp.value,maxTemp.value);
        adjustTemperatureMarker();
    }

    public void TempDown() {
        Temperature.value--;
        Temperature.value = Mathf.Clamp(Temperature.value, minTemp.value,maxTemp.value);
        adjustTemperatureMarker();
    }

    public void adjustTemperatureMarker() {
        tParam = 0f;
        float percent = (Temperature.value - minTemp.value) / (maxTemp.value - minTemp.value);
        Debug.Log("percent: " + percent);
        float maxMarkerDistance = maxMarker.localPosition.y - minMarker.localPosition.y;
        Debug.Log("max marker distance: " + maxMarkerDistance);
        Debug.Log("current y pos" + tempMarker.localPosition.y); 
        newPos = new Vector3(tempMarker.localPosition.x, minMarker.localPosition.y + Mathf.Abs(maxMarkerDistance * percent) ,tempMarker.localPosition.z);
        Debug.Log("new pos: " + newPos);
    }
    
    // turn steering wheel
    public void turnWheel(InputAction.CallbackContext ctx) {
        float inputValue = ctx.ReadValue<float>();
        int direction = 0;
        if (inputValue > 0) {
            Debug.Log("right");
            direction = 1;
        }
        else if (inputValue < 0) {
            Debug.Log("left");
            direction = -1;
        }
        
        wheelAnimator.SetInteger("Direction", direction);
    }
    
    

}
// type
public enum RadioStation {
    Station_69,
    Station_97,
    Station_102,
    Station_111,
    Station_128,
}
