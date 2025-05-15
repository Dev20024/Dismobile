using UnityEngine;

public class PatrollingState : GroundState {
    
    private bool direction;
    private int movementTokens;

    protected override void Enter()
    {
       pickDirection();
    }

    protected override void PhysicsTick()
    {
        manager.transform.position += (manager.ObjectiveNode.position - manager.transform.position).normalized * manager.speed * Time.deltaTime;
        if ((manager.ObjectiveNode.position - manager.transform.position).magnitude < 3f) {
            ObjectiveNodeReached();
        }
    }

    private void ObjectiveNodeReached() {
        movementTokens -= 1;
        if (movementTokens <= 0) {pickDirection();}

        if (direction == true && manager.ObjectiveNode.nextNode != null) {
             manager.ObjectiveNode = manager.ObjectiveNode.nextNode;
        }
        else if (manager.ObjectiveNode.previousNode != null) {
             
             manager.ObjectiveNode = manager.ObjectiveNode.previousNode;
        }

        manager.parentChunk = manager.ObjectiveNode.parentChunk;
    }

    private void pickDirection() {
        int randomNum = Random.Range(0,2);
        direction = (randomNum == 1) ? true : false;
        movementTokens = Random.Range(5,25);
    }
}