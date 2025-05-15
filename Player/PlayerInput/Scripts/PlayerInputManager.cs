using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // action map
    PlayerInputMap inputMap;
    // action types
    public PlayerInputMap.DrivingActions driving;
    public PlayerInputMap.AdjustingCarActions adjustCar;
    public PlayerInputMap.DialogueActions dialogue;
    public PlayerInputMap.InApartmentActions inApartment;
    
    // input events
    [Header("Driving Events : Physics")]
    [SerializeField] InputEvent Accelerate;
    [SerializeField] InputEvent Break;
    [SerializeField] InputEvent turn;
 //   [Header("Driving Events : Adjustments")]
   // [SerializeField] InputEvent AdjustTemperature;
  //  [SerializeField] InputEvent AdjustStation;
 //   [SerializeField] InputEvent AdjustWindows;
    [Header("Driving Events : Dialogue")]
    [SerializeField] InputEvent ContinueDialogue;

    private void Awake() {
         inputMap = new PlayerInputMap();
         driving = inputMap.Driving;
         adjustCar = inputMap.AdjustingCar;
         dialogue = inputMap.Dialogue;
         inApartment = inputMap.InApartment;
    }

    void OnEnable() {
        
        driving.Enable();
        adjustCar.Enable();
        dialogue.Enable();
        //  driving events
            // physics driven events
            driving.Accelerate.started += (InputAction.CallbackContext ctx) => {Accelerate?.Fire(ctx);};
            driving.Accelerate.canceled += (InputAction.CallbackContext ctx) => {Accelerate?.Fire(ctx);};
            
            driving.Breaking.started +=(InputAction.CallbackContext ctx) => {Break?.Fire(ctx);};
            driving.Breaking.canceled +=(InputAction.CallbackContext ctx) => {Break?.Fire(ctx);};

            driving.Turning.started +=(InputAction.CallbackContext ctx) => {turn?.Fire(ctx);};
            driving.Turning.canceled +=(InputAction.CallbackContext ctx) => {turn?.Fire(ctx);};
            // interior attribute events
          //  adjustCar.AdjustTemperature.performed += (InputAction.CallbackContext ctx) => { AdjustTemperature?.Fire(ctx);};
          //  adjustCar.AdjustStation.performed += (InputAction.CallbackContext ctx) => {AdjustStation?.Fire(ctx);};
          //  adjustCar.AdjustWindows.performed += (InputAction.CallbackContext ctx) => {AdjustWindows?.Fire(ctx);};
        // dialogue events
        dialogue.Continue.performed += (InputAction.CallbackContext ctx) => {ContinueDialogue?.Fire(ctx);};
        //  apartment events
            
    }

    private void OnDisable() {
        driving.Disable();
        adjustCar.Disable();
        dialogue.Disable();
       // adjustCar.AdjustTemperature.performed -= (InputAction.CallbackContext ctx) => {AdjustTemperature?.Fire(ctx);};
       // adjustCar.AdjustStation.performed -= (InputAction.CallbackContext ctx) => {AdjustStation?.Fire(ctx);};
        driving.Accelerate.started -= (InputAction.CallbackContext ctx) => {Accelerate?.Fire(ctx);};
        driving.Accelerate.canceled -= (InputAction.CallbackContext ctx) => {Accelerate?.Fire(ctx);};
        driving.Breaking.started -=(InputAction.CallbackContext ctx) => {Break?.Fire(ctx);};
        driving.Breaking.canceled -=(InputAction.CallbackContext ctx) => {Break?.Fire(ctx);};
        driving.Turning.started -=(InputAction.CallbackContext ctx) => {turn?.Fire(ctx);};
        driving.Turning.canceled -=(InputAction.CallbackContext ctx) => {turn?.Fire(ctx);};
        dialogue.Continue.performed -= (InputAction.CallbackContext ctx) => {ContinueDialogue?.Fire(ctx);};
    }

    

    
}
