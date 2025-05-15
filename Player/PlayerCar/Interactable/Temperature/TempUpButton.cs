using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUpButton : MonoBehaviour, _interactable
{
    public static event Action OnTempUp;

    public void OnInteract() {
        OnTempUp?.Invoke();
    }
}
