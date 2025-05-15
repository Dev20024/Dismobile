using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraScr : MonoBehaviour
{
    // camera variables
    protected Camera cam;
    // camera events
    

    // start function
    private void Start() {
        cam = GetComponent<Camera>();
        OnStart();
    }
    protected virtual void OnStart() {}



    // public defualt functions
    public virtual void SetPos(Vector3 newPos) {
        transform.position = newPos;
    }

    public virtual void SetParent(Transform newParent) {
        transform.parent = newParent;
    }
}
