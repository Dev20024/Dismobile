using UnityEngine;


public class PassiveDrivingState : CarState {


    protected override void Tick()
    {
       
    }

    protected override void PhysicsTick() {
        float distance = (manager.associatedNode.position - manager.transform.position).magnitude;
        Vector3 direction = (manager.associatedNode.position - manager.transform.position).normalized;
        manager.carController.targetDirection = direction;
        manager.carController.targetVelocity = manager.targetVelocity;

        if (distance < 3f) {
            ObjectiveNodeReached();
        }
    }

    private void ObjectiveNodeReached() {
        if (manager.associatedNode.nextNode == null) {return;}
        manager.associatedNode = manager.associatedNode.nextNode;
        manager.parentChunk = manager.associatedNode.parentChunk;
        manager.setSpeed(manager.associatedNode.speedLimit);
    }
}