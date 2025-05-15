using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new Money Event", menuName = "Game/Events/Money Event")]
public class MoneyEvent : ScriptableObject
{
    private List<MoneyEventListener> listerners = new List<MoneyEventListener>();
    // Start is called before the first frame update
    
    public void RegisterListener(MoneyEventListener listener) { 
        listerners.Add(listener);
    }

    public void UnRegisterListener(MoneyEventListener listener) { 
        listerners.Remove(listener);
    }

    public void Fire(transactionDataPack transaction) {
        for (int i=listerners.Count - 1; i >=0; i--) {
            listerners[i].OnEventRaised(transaction);
        }
    }


}
