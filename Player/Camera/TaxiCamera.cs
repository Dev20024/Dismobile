using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaxiCamera : CameraScr
{
    // Camera tracker
    float rotationX = 0f;
    float rotationY = 0f;
    // Camera settings
    public float sensitivity = 15f;
    public float maxY = 75;
    public float maxX = 220;

    protected override void OnStart() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Vector2 mousePos = Mouse.current.delta.ReadValue();
        rotationX = rotationX += mousePos.x * sensitivity * Time.deltaTime;
        rotationY = rotationY += mousePos.y * sensitivity * Time.deltaTime;
        cam.transform.localEulerAngles = new Vector3(-rotationY,rotationX, 0);
    }

    

    


}
