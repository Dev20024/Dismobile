using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDownButton : MonoBehaviour, _interactable
{
    public static event Action OnTempDown; 

    public void OnInteract() {
        OnTempDown?.Invoke();
    }
}
