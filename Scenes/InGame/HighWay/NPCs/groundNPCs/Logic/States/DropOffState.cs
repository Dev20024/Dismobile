using UnityEngine;

public class DropOffState : GroundState {

    PlayerCar playerCar;

    protected override void Enter()
    {
        DialogueManager.GetInstance().EnterDialogue(manager.Exitdialogue, "Test man");
        manager.objectiveTr.TryGetComponent<PlayerCar>(out playerCar);
        if (playerCar == null) {
            Debug.Log("player car non existant");
        }
    }

    Vector3 previousPos;
    protected override void PhysicsTick()
    {
        Vector3 CurrentPos = transform.position;
               float velocity = (CurrentPos - previousPos).magnitude / Time.deltaTime;
               previousPos = CurrentPos;
               if (Mathf.RoundToInt(velocity) == 0) {
                    PlayerEvents.playerPayOut?.Invoke(manager.payOut);
                    playerCar.isPassenger = false;
                    manager.End();
               }
    }

      

         
}