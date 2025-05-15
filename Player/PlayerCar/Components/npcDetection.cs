using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class npcDetection : MonoBehaviour
{
    [Header("Stats Tracking")]
    [SerializeField] bool npcDetected;
    RaycastHit npcHit;
    Transform lastNPCDetected;
    [Header("Properties")]
    [SerializeField] float maxDistance = 3;
    [SerializeField] LayerMask lookingFor;
    // events
    public delegate void OnNPCFound(Transform npcTR, Transform subjectTR);
    public static event OnNPCFound onNPCFound;

    public void checkTaxiSides() {
        if (npcDetected) {return;}
     //  Debug.Log("checking taxi's sides");
       npcDetected = Physics.BoxCast(transform.position, transform.localScale*0.5f, -transform.right, out npcHit, transform.rotation, maxDistance, lookingFor);
       if (!npcDetected) { return;}
       Debug.Log("npc detected");
       Debug.Log(npcHit.transform.name);
       lastNPCDetected = npcHit.transform;
       onNPCFound?.Invoke(lastNPCDetected, transform);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Draw a cube at the maximum distance
        Gizmos.DrawWireCube(transform.position + transform.forward * maxDistance, transform.localScale);
    }
}
