using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour, _interactable
{

    [SerializeField] Transform newView;
    // change view request
    public delegate void OnChangeView(Transform transform);
    public static event OnChangeView onChangeView;


    private void Start() {
        
    }

    public void OnInteract() {
       onChangeView?.Invoke(newView);
    }


}
