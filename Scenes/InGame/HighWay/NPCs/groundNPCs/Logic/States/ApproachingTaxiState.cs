using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ApproachingTaxiState : GroundState {


    // Taxi Current State
    PlayerCar playerCar;
    Transform taxi;
    // Moving Towards Taxi Calculations
    Vector3 difference;
    float distance;
    Vector3 direction;

    protected override void Enter() {
        Debug.Log("moving towards taxi");
        taxi = manager.objectiveTr;
        taxi.TryGetComponent<PlayerCar>(out playerCar);
        if (taxi == null || playerCar == null) {manager.SwitchStates(new WaitingForTaxiState());}
    }

    protected override void PhysicsTick()
    {
        // Calculate path towards Taxi.
        difference = (taxi.position - transform.position);
        distance = difference.magnitude;
        direction = difference.normalized;
        // Compute movement and proximity to taxi.
        moveTowardsTaxi();
        
        checkProximity();
    }
    
    // move the npc towards the taxi each frame.
    private void moveTowardsTaxi() {
        transform.position += direction * manager.speed * Time.deltaTime;
    }

    // if the distance between the NPC and taxi is less than 3, then NPC 'enters' the taxi.
    private void checkProximity() {
         if (distance < 3f) {
            manager.SwitchStates(new inTaxiState());
         } 
    }
                
    
    
    
}