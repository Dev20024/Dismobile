
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class RoadInfo : MonoBehaviour
{
    // buildings
    public List<Transform> BuildingNodes = new List<Transform>();  
    // NPC Walking Nodes
    public StringTransformGroupPair[] WalkingNodes;
    // NPC Driving Nodes
    public StringTransformGroupPair[] DrivingNodes;
    // Taxi NPC spawners
    public Transform[] taxiNPCSpawners;
}
