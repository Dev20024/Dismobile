using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InApartment : PlayerState
{
    public override string intendedScene { get => "Apartment"; protected set => base.intendedScene = "Apartment"; }

    protected override void OnEnter()
    {
        Debug.Log("Player has entered the apartment");
        playerInputManager.inApartment.Enable();
    }

    protected override void OnExit()
    {
        Debug.Log("Player has left the apartment");
        playerInputManager.inApartment.Disable();
    }
}
