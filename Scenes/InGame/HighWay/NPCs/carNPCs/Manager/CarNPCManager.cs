using System.Collections.Generic;
using UnityEngine;


public class CarNPCManager {
     // List of current Car NPCS
    public static List<CarNPC> carNPCS;
    // List of the each Driving Lane name and its respective Walking Lane Object.
    static Dictionary<string,Lane<DrivingNode>> drivingLanes = new Dictionary<string,Lane<DrivingNode>>();
    
     // Initializes a certain number of walking lanes at the start of the game.
    public static void AddWalkingLanes(int numberOfLanes) {
        Debug.Log("Adding Driving Lanes");
        for (int i = 0; i < numberOfLanes - 1; i++) {
            string laneName = "Lane " + i.ToString();
            Lane<DrivingNode> newLane = new Lane<DrivingNode>(laneName);
            
            drivingLanes.Add(laneName, newLane);
            Debug.Log(laneName + " has been added");
        }
    }

    
    public static void loadDrivabeChunk(RoadInfo roadInfo, Chunk parentChunk) {
        Debug.Log("adding driving nodes");
        
        StringTransformGroupPair[] nodeGroupSet = roadInfo.DrivingNodes;
        Transform[] taxiNPCSpawners = roadInfo.taxiNPCSpawners;

        // load walking nodes
        UnpackDrivingNodes(nodeGroupSet, parentChunk);
        // loads NPC Spawners
        UnpackTaxiNPCSpawners(taxiNPCSpawners, parentChunk);
    
    }

    /* 
        Unpacks road info's walking node transforms, 
        Converts Walking Node Transforms into Walking Node Objects incorperated into their own lanes,
        Creates an additional Walking NPC spawner at the end of each chunk per lane.
    */
    public static void UnpackDrivingNodes(StringTransformGroupPair[] nodeGroupSet, Chunk parentChunk) {
        // loop through each set of transforms correlating to walking nodes in a road chunk
        foreach (StringTransformGroupPair nodeGroup in nodeGroupSet) {
            // current lane
            Lane<DrivingNode> thisLane = drivingLanes[nodeGroup.key];
            Debug.Log("Currently on " + thisLane.name);
            if (thisLane == null) {Debug.LogWarning("no lane exists for this node group..."); continue;}

            // convert tranform nodes into walking node objects
            foreach (Transform nodeTR in nodeGroup.value) {
                
                DrivingNode newNode = new DrivingNode(nodeTR.position, parentChunk, thisLane.lastLoadedNode);
                thisLane.nodes.Add(newNode);
                parentChunk.drivingNodes.Add(newNode);
                // assign "node order" to previous walking nodes
                if (thisLane.lastLoadedNode != null) {
                    thisLane.lastLoadedNode.nextNode = newNode;
                }
                thisLane.lastLoadedNode = newNode;
                //Debug.Log("added a node to " + thisLane.name);
            }
            // add a NPC spawner for this walking lane at the end of this road chunk
            DrivingNode lastNode = thisLane.lastLoadedNode;
             newSpawner = new GroundNPCSpawner(lastNode);
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