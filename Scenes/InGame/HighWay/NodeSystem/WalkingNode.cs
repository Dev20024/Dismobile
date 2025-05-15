
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WalkingNode : BaseNode{
    
    // surrounding Nodes
    public WalkingNode previousNode;
    public WalkingNode nextNode;
    //public WalkingLane nodeGroup;

    public WalkingNode(Vector3 position, Chunk parentChunk, WalkingNode previousNode) {
        base.Init(position,parentChunk);
        this.previousNode = previousNode;
    }

    public void unLoad() {
        previousNode = null;
        nextNode = null;
    }

}