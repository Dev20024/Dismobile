using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaitingForTaxiState : GroundState
{
    
    
    protected override void Enter() {
        Debug.Log("entered waiting for taxi state");
        npcDetection.onNPCFound += enterTaxiSearch;
    }

    protected override void Exit() {
        Debug.Log("exiting waiting for taxi state");
        npcDetection.onNPCFound -= enterTaxiSearch;
    }


    private void enterTaxiSearch(Transform npcTr, Transform playerCarTr) {
        Debug.Log("taxi transform detect: " + npcTr + " This npc is: " + manager.characterModel.transform);
        if (!manager.characterModel.transform.Equals(npcTr)) {return;}
        manager.ObjectiveNode = null;
        manager.objectiveTr = playerCarTr;
        manager.SwitchStates(new ApproachingTaxiState());
    }
}
