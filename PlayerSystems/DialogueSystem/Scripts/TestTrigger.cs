using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTrigger : MonoBehaviour
{
    
    [SerializeField] TextAsset inkJSON;

    

    public void startDialogue() {
        Debug.Log("test button clicked");
        DialogueManager.GetInstance().EnterDialogue(inkJSON, "Test man");
    } 
}
