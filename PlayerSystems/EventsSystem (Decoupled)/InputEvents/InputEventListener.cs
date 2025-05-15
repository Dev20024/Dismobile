using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputEventListener : MonoBehaviour
{
    // GameEvent to listen to
    public InputEvent Event;
    // Response when GameEvent is fired
    public UnityEvent<InputAction.CallbackContext> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(InputAction.CallbackContext ctx) {
        Response.Invoke(ctx);
    }

}
