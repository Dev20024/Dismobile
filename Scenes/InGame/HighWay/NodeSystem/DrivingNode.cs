using Unity.VisualScripting;
using UnityEngine;

public class DrivingNode : BaseNode {
    
    // properties
    public int speedLimit;
    public bool stop;
    // surrounding nodes
    public DrivingNode previousNode;
    public DrivingNode nextNode;
    public Lane<DrivingNode> lane;

    public DrivingNode (Vector3 position, Chunk parentChunk, DrivingNode previousNode) {
        base.Init(position,parentChunk);
        this.previousNode = previousNode;
    }

    public void UnLoad() {
        previousNode = null;
        nextNode = null;
    }
}