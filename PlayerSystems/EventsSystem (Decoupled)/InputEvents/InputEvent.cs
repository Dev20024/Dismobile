using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new Input Event", menuName = "Game/Events/Input Event")]
public class InputEvent : ScriptableObject
{
    private List<InputEventListener> listerners = new List<InputEventListener>();
    // Start is called before the first frame update
    
    public void RegisterListener(InputEventListener listener) { 
        listerners.Add(listener);
    }

    public void UnRegisterListener(InputEventListener listener) { 
        listerners.Remove(listener);
    }

    public void Fire(InputAction.CallbackContext ctx) {
        for (int i=listerners.Count - 1; i >=0; i--) {
            listerners[i].OnEventRaised(ctx);
        }
    }
}
