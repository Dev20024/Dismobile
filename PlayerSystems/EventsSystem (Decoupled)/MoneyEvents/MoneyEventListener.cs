using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoneyEventListener : MonoBehaviour
{
    // GameEvent to listen to
    public MoneyEvent Event;
    // Response when GameEvent is fired
    public UnityEvent<transactionDataPack> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(transactionDataPack transaction) {
        Response.Invoke(transaction);
    }


}
