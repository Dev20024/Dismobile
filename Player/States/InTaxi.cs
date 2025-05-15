using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTaxi : PlayerState
{
    public override string intendedScene { get => "Highway"; protected set => base.intendedScene = "Highway"; }

    protected override void OnEnter()
    {
        Debug.Log("Player is in the taxi");
        playerInputManager.adjustCar.Enable();
        playerInputManager.driving.Enable();
    }

    protected override void OnExit()
    {
        Debug.Log("player is no longer in the taxi");
        playerInputManager.adjustCar.Disable();
        playerInputManager.driving.Disable();
    }
}
