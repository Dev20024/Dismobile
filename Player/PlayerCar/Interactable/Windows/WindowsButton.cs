using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsButton : MonoBehaviour, _interactable
{
    public static event Action OnWindowsChange;

    public void OnInteract() {
        OnWindowsChange?.Invoke();
    }
}
