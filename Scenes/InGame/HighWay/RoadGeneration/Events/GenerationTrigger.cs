using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class GenerationTrigger : MonoBehaviour
{
public delegate void OnGenerationEvent();
public static event OnGenerationEvent onGenerationEvent;


private void OnTriggerEnter(Collider other) {
    if (other.gameObject.layer == LayerMask.NameToLayer("Player Vehicle")) {
        onGenerationEvent.Invoke();
    }
}

}