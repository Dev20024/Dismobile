using System;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;
using UnityEngine.PlayerLoop;

public static class WalkingNPCManager {

    // List of current ground NPCS
    public static List<GroundNPC> groundNPCS;
    // List of the each Walking Lane name and its respective Walking Lane Object.
    static Dictionary<string,Lane<WalkingNode>> walkingLanes = new Dictionary<string,Lane<WalkingNode>>();
    

    // Initializes a certain number of walking lanes at the start of the game.
    public static void AddWalkingLanes(int numberOfLanes) {
        Debug.Log("Adding Walking Lanes");
        for (int i = 0; i < numberOfLanes - 1; i++) {
            string laneName = "Lane " + i.ToString();
            Lane<WalkingNode> newLane = new Lane<WalkingNode>(laneName);
            
            walkingLanes.Add(laneName, newLane);
            Debug.Log(laneName + " has been added");
        }
    }

    
    public static void loadWalkableChunk(RoadInfo roadInfo, Chunk parentChunk) {
        Debug.Log("adding walking nodes");
        
        StringTransformGroupPair[] nodeGroupSet = roadInfo.WalkingNodes;
        Transform[] taxiNPCSpawners = roadInfo.taxiNPCSpawners;

        // load walking nodes
        UnpackWalkingNodes(nodeGroupSet, parentChunk);
        // loads NPC Spawners
        UnpackTaxiNPCSpawners(taxiNPCSpawners, parentChunk);
    
    }

    /* 
        Unpacks road info's walking node transforms, 
        Converts Walking Node Transforms into Walking Node Objects incorperated into their own lanes,
        Creates an additional Walking NPC spawner at the end of each chunk per lane.
    */
    public static void UnpackWalkingNodes(StringTransformGroupPair[] nodeGroupSet, Chunk parentChunk) {
        // loop through each set of transforms correlating to walking nodes in a road chunk
        foreach (StringTransformGroupPair nodeGroup in nodeGroupSet) {
            // current lane
            Lane<WalkingNode> thisLane = walkingLanes[nodeGroup.key];
            Debug.Log("Currently on " + thisLane.name);
            if (thisLane == null) {Debug.LogWarning("no lane exists for this node group..."); continue;}

            // convert tranform nodes into walking node objects
            foreach (Transform nodeTR in nodeGroup.value) {
                
                WalkingNode newNode = new WalkingNode(nodeTR.position, parentChunk, thisLane.lastLoadedNode);
                thisLane.nodes.Add(newNode);
                parentChunk.walkingNodes.Add(newNode);
                // assign "node order" to previous walking nodes
                if (thisLane.lastLoadedNode != null) {
                    thisLane.lastLoadedNode.nextNode = newNode;
                }
                thisLane.lastLoadedNode = newNode;
                //Debug.Log("added a node to " + thisLane.name);
            }
            // add a NPC spawner for this walking lane at the end of this road chunk
            WalkingNode lastNode = thisLane.lastLoadedNode;
            GroundNPCSpawner newSpawner = new GroundNPCSpawner(lastNode);
            newSpawner.spawnChancesCont = RoadManager.instance.currentBiome.walkingNPCSpawnChances;
            newSpawner.Spawn();
        }
    }
    
    /*
        Unpacks Taxi NPC Spawner Transforms,
        Converts Taxi NPC Spawner transforms into object npc spawners.
    */
    public static void UnpackTaxiNPCSpawners(Transform[] taxiNPCSpawners, Chunk parentChunk) {
        if (taxiNPCSpawners == null) {return;}
        // loop through each spawner Transform and convert it to a object npc spawner
        foreach (Transform spawnerTr in taxiNPCSpawners) {
            GroundNPCSpawner newSpawner = new GroundNPCSpawner();
            newSpawner.position = spawnerTr.position;
            newSpawner.parentChunk = parentChunk;
            newSpawner.spawnChancesCont = RoadManager.instance.currentBiome.walkingNPCSpawnChances;
            newSpawner.startingState = new WaitingForTaxiState();
            newSpawner.Spawn();
        }
}

}

// Types of starting states that Ground NPCS can spawn with.





