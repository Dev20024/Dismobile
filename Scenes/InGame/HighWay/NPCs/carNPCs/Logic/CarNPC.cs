using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(PrimitiveCarController))]
public class CarNPC : MonoBehaviour {

    [Header("Car Attributes")]
    public Driver type;
    [Header("Speed")] 
    public int targetVelocity;
    [Header("States")]
    public CarState CurrentState;
    [Header("Status")]
    public DrivingNode associatedNode;
    public Chunk parentChunk;
    [Header("Component")]
    public PrimitiveCarController carController;

    void Start()
    {
        carController.maxAcceleration = type.acceleration;
        carController.peakAccelTime = type.accelTime;
    }

    public void SwitchStates(CarState newState) {
        if (CurrentState != null) {CurrentState.OnExit();}
        Debug.Log(newState);
        CurrentState = newState;
        Debug.Log(CurrentState);
        CurrentState.OnEnter(this);
    }

    void Update()
    {
        CurrentState.OnTick();
    }

    void FixedUpdate()
    {
        CurrentState.OnPhysicsTick();  
    }

    
    public void setSpeed(int suggestedSpeed) {
        targetVelocity = suggestedSpeed + type.speedDeviation;
    }

    

    
}