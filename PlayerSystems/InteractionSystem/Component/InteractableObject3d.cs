using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


public class InteractableObject3d : MonoBehaviour, _interactableObject
{
    // object properties
    private _interactable objectClass;
    private MeshRenderer meshRenderer;
    private Material interactiveMaterial3d;
    // events
    public GameEvent cursorHover;
    

    // initialize object and 2d or 3d renderer
    private void Start() {
        
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
        meshRenderer = GetComponent<MeshRenderer>();
        Material[] interactiveMaterials = new Material[meshRenderer.materials.Length + 1];
        int index = 0;
        foreach (Material m in meshRenderer.materials) {
            interactiveMaterials[index] = m;
            index++;
        }
        interactiveMaterials[interactiveMaterials.Length - 1] = Resources.Load("InteractionOutline3d", typeof(Material)) as Material;
        meshRenderer.materials = interactiveMaterials;
        interactiveMaterial3d = meshRenderer.materials[meshRenderer.materials.Length - 1];
    }

    private void LoadLayer() {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    // mouse related functionality
    public void MouseOn() {
        cursorHover?.Fire();
        
            Debug.Log("is 3d");
            interactiveMaterial3d.SetFloat("_On",1);
    
        Debug.Log("hovering over " + transform.gameObject.name);
    }

    public void MouseOff(){
        cursorHover?.Fire();
        
        interactiveMaterial3d.SetFloat("_On",0);
        
        Debug.Log("mouse no longer hovering over " + transform.gameObject.name);
    }

    public void MouseClick() {
        if (objectClass == null){return;}
        objectClass.OnInteract();
    } 

}
