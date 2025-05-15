using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdvancedCarController : MonoBehaviour {
    public WheelScript[] wheels;

    [Header("Characteristics")]
    public float steerAngle;
    public float steerTime = 8;
    public float tireGripFactor = 1;
    public float accelPower = 2000;
    public float maxSpeed = 50;
    public float BreakingPower = 500;
    public float TurningPower = 10000;
    [Header("Inputs")]
    public float accelerationInput;
    public float turnInput;
    public bool isBreaking;

    

    // input events
    public void onAccel(InputAction.CallbackContext ctx) {
        accelerationInput = ctx.ReadValue<float>();
    }

    public void onTurn(InputAction.CallbackContext ctx) {
        turnInput = ctx.ReadValue<float>();
    }

    public void onBreak(InputAction.CallbackContext ctx) {
       if (ctx.started) {
            isBreaking = true;
            
       }
       if (ctx.canceled) {
            isBreaking = false;
       }
       
    }

    

    
}
