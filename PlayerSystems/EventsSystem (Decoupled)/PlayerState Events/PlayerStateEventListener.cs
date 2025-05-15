using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateEventListener : MonoBehaviour
{
    // GameEvent to listen to
    public PlayerStateEvent Event;
    // Response when GameEvent is fired
    public UnityEvent<PlayerState> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(PlayerState newState) {
        Response.Invoke(newState);
    }

}
