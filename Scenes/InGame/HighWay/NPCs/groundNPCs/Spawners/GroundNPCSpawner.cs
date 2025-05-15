
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundNPCSpawner
{
    
    // Spawner Attributes
    public Vector3 position;
    public Chunk parentChunk;
    // Node group
    public WalkingNode associatedNode;
    // Spawner Settings
    public SpawnChances_GroundNPC spawnChancesCont;
    // Last Spawned NPC
    public GroundState startingState;

    public void Spawn() {
        Debug.Log("spawning a new NPC!");
        // instantiate new NPC Transform
        GameObject NewNPCHolder = new GameObject("NPC");
        NewNPCHolder.transform.position = position;
        NewNPCHolder.transform.rotation = Quaternion.identity;
       // Object.Instantiate(new GameObject(),position, Quaternion.identity);
        //NewNPCHolder.name = "NPC";
        // init new NPC Logic
        
        GroundNPC newNPC = NewNPCHolder.AddComponent<GroundNPC>();
        newNPC.type = chooseNPCType();
        newNPC.parentChunk = parentChunk;
        newNPC.ObjectiveNode = associatedNode;
        newNPC.Init(startingState, position);
        Debug.Log("Assigned to Chunk: " + parentChunk);
        

    }

    private Character chooseNPCType() {
        Character chosenCharacter = null;
        int totalWeight = 0;
        foreach  (WeightCharacterPair chance in spawnChancesCont.spawnChances) {
            totalWeight += chance.weight;
        }
        Debug.Log("total weight: " + totalWeight);
        int randomNum = Random.Range(0,totalWeight);
        int cumulativeChance = 0;

        Debug.Log("random number: " + randomNum);

        foreach (WeightCharacterPair chance in spawnChancesCont.spawnChances) {
            Debug.Log(chance.type);
            if (randomNum <= chance.weight + cumulativeChance) {chosenCharacter = chance.type;  break;}
            cumulativeChance += chance.weight;
        }

        Debug.Log(chosenCharacter);
        return chosenCharacter;
        
    }

    public GroundNPCSpawner () {}

    public GroundNPCSpawner(Vector3 position, Chunk parentChunk, SpawnChances_GroundNPC spawnChances, GroundState startingState) {
        this.position = position;
        this.parentChunk = parentChunk;
        this.spawnChancesCont = spawnChances;
        this.startingState = startingState;
    }

    public GroundNPCSpawner(WalkingNode associatedNode) {
        this.associatedNode = associatedNode;
        position = associatedNode.position;
        parentChunk = associatedNode.parentChunk;
        startingState = new PatrollingState();
    }
}
