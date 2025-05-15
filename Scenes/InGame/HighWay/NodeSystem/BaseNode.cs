using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseNode {
    public Vector3 position;
    public Chunk parentChunk;

    protected void Init(Vector3 position, Chunk parentChunk) {
        this.position = position;
        this.parentChunk = parentChunk;
    }
}