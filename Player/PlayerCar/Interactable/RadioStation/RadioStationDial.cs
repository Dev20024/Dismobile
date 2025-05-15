using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioStationDial : MonoBehaviour, _interactable
{
    public static event Action OnDial;

    public void OnInteract() {
        Debug.Log("Radio Station Changed");
        OnDial?.Invoke();
    }
}
