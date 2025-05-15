using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject2d : MonoBehaviour, _interactableObject
{
    // object properties
    private _interactable objectClass;
    private SpriteRenderer spriteRenderer;
    private Material interactiveMaterial2d;
    // events
    public GameEvent cursorHover;


    // Start is called before the first frame update
    void Start()
    {
         if (TryGetComponent<_interactable>(out objectClass) ) {
            LoadMaterial();
            LoadLayer();
        }
        else {
            Debug.LogWarning("No object class using _interactable found!");
        }

    }

    // setting up interactable object
    private void LoadMaterial() {
        spriteRenderer  = GetComponent<SpriteRenderer>();
        interactiveMaterial2d = spriteRenderer.material;
    }

    private void LoadLayer() {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }


     // mouse related functionality
    public void MouseOn() {
        cursorHover?.Fire();
        
            Debug.Log("is 2d");
            interactiveMaterial2d.SetFloat("_On",1);
    
        Debug.Log("hovering over " + transform.gameObject.name);
    }

    public void MouseOff(){
        cursorHover?.Fire();
        
        interactiveMaterial2d.SetFloat("_On",0);
        
        Debug.Log("mouse no longer hovering over " + transform.gameObject.name);
    }

    public void MouseClick() {
        if (objectClass == null){return;}
        objectClass.OnInteract();
    } 

    
}
