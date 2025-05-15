
using UnityEngine;
using UnityEngine.UIElements;

public class GroundNPC : MonoBehaviour 
{
    [Header("Attributes")]
    public Character type;
    public GameObject characterModel;
    public TextAsset dialoguePool;
    public TextAsset Exitdialogue;
    public int speed = 3;
    [Header("State")]
    public GroundState CurrentState;
    [Header("Status")]
    public Chunk parentChunk;
    public WalkingNode ObjectiveNode;
    public Transform objectiveTr;
    public transactionDataPack payOut;

    // base 
    private void Start() {
        //NPCManager.groundNPCS.Add(this);
       // Spawn(new WaitingForTaxiState());
    }

    // interface funcs
    public void Init(GroundState startingState, Vector3 spawnPos) {
        characterModel = Instantiate(type.characterModel,spawnPos,Quaternion.identity,transform);
        dialoguePool = type.dialoguePool;
        SwitchStates(startingState);
    }

    public void DeSpawn() {
        Debug.Log("Despawning...");
        if (CurrentState != null) {CurrentState.OnExit();}
        Destroy(transform.gameObject);
    }

    public void SwitchStates(GroundState newState) {
        if (CurrentState != null) {CurrentState.OnExit();}
        Debug.Log(newState);
        CurrentState = newState;
        Debug.Log(CurrentState);
        CurrentState.OnEnter(this);
    }

    public void End() {
        Destroy(this.gameObject);
    }

    // inherited funcs  
    private void Update() {
        if (CurrentState == null) {return;}
        CurrentState.OnTick();
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.green);

        if (!parentChunk.active) {
            DeSpawn();
        }

    }

    private void FixedUpdate() {
        if (CurrentState == null) {return;}
        CurrentState.OnPhysicsTick();
    }

    
    
    
    

}
