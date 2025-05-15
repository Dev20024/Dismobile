using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Game Event", menuName = "Game/Events/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listerners = new List<GameEventListener>();
    // Start is called before the first frame update
    
    public void RegisterListener(GameEventListener listener) { 
        listerners.Add(listener);
    }

     public void UnRegisterListener(GameEventListener listener) { 
        listerners.Remove(listener);
    }

    public void Fire() {
        for (int i=listerners.Count - 1; i >=0; i--) {
            listerners[i].OnEventRaised();
        }
    }
}
