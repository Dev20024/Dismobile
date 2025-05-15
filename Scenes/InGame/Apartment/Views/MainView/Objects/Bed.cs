using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bed : MonoBehaviour, _interactable
{
    public GameEvent StartNextDay;

    public void OnInteract() {
        Debug.Log("Bed has been interacted with");
        StartNextDay?.Fire();
    }
}
