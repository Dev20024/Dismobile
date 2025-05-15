using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerState Event", menuName = "Game/Events/PlayerState Event")]
public class PlayerStateEvent : ScriptableObject
{
    private List<PlayerStateEventListener> listerners = new List<PlayerStateEventListener>();
    // Start is called before the first frame update
    
    public void RegisterListener(PlayerStateEventListener listener) { 
        listerners.Add(listener);
    }

     public void UnRegisterListener(PlayerStateEventListener listener) { 
        listerners.Remove(listener);
    }

    public void Fire(PlayerState newState) {
        for (int i=listerners.Count - 1; i >=0; i--) {
            listerners[i].OnEventRaised(newState);
        }
    }
}
