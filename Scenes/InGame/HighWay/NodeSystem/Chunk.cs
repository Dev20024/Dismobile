
using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Chunk {
    // attributes
    public Vector3 position = Vector3.zero;
    public GameObject road;
    public bool active = true;
    // contains
    public List<WalkingNode> walkingNodes = new List<WalkingNode>();
    public List<GroundNPCSpawner> npcSpawners = new List<GroundNPCSpawner>();
    public List<DrivingNode> drivingNodes = new List<DrivingNode>();
    // surrounding chunks
    public Chunk previousChunk;
    public Chunk nextChunk;

    // events
    public delegate void OnChunkUnLoad(Chunk chunk);
    public static OnChunkUnLoad onChunkUnLoad;


    public Chunk() {}

    public void UnLoad() {
        active = false;
        GameObject.Destroy(road);
       // onChunkUnLoad?.Invoke(this);
       // foreach (GroundNPC npc in npcs) {
        //    npc.DeSpawn();
        //}
        for (int i = 0; i < walkingNodes.Count - 1; i++ ) {
            WalkingNode walkingNode = walkingNodes[i];
            walkingNode.unLoad();
            
        }
        for (int i = 0; i < npcSpawners.Count - 1; i++ ) {
            GroundNPCSpawner spawner = npcSpawners[i];
            spawner.associatedNode = null;
            
        }
    
    }

}