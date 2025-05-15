using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentManager : MonoBehaviour
{

    // singleton setup
    public static ApartmentManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    // events
    private void OnEnable() {
        ViewSwitcher.onChangeView += switchViews;
    }

    private void OnDisable() {
        ViewSwitcher.onChangeView -= switchViews;
    }

    CameraScr currentCam;

    private void Start() {
        currentCam = Camera.main.GetComponent<CameraScr>();
    }

    public void switchViews(Transform newCamTransform) {
        currentCam.SetPos(newCamTransform.position);
    }
}
