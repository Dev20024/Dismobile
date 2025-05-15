using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{

    Camera cam;
    private LayerMask mask;
    void Start()
    {
        cam = GetComponent<Camera>();
        mask =  LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    _interactableObject currentObj;

    // mouse hovering logic
    void FixedUpdate()
    {
        // ray cast variables
       RaycastHit hit;
       Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
       Debug.DrawRay(ray.origin, ray.direction  * 5.5f,Color.red);
       bool isInteractable = Physics.Raycast(ray,out hit, 5.5f, mask);
        // check current object
        if (!isInteractable) { 
            if (currentObj != null) {currentObj.MouseOff(); currentObj = null;}
            return;
        }
        Debug.Log(isInteractable);
        Debug.Log("hitting interactable object");
        // get new interactable object
        _interactableObject hitObject = hit.collider.GetComponent<_interactableObject>();
        if (currentObj != null) {return;}
        Debug.Log("switching interactable object");
        // switching interactable objects
        if (currentObj != null) {currentObj.MouseOff();}
        currentObj = hitObject;
        currentObj.MouseOn();
    }

    // mouse interacting logic
    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            if (currentObj != null) {
                currentObj.MouseClick();
            }
        }
    }

}
