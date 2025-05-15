
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class inTaxiState : GroundState
{
    // player car
    PlayerCar playerCar;
    
    // preferences
    [Header("Preferences")]
    public float preferenceBonus;
    public int preferenceCap;
    public int tempBonus;
    public int stationBonus;
    public int windowsBonus;
    [Header("Therapy Status")]
    public float therapyBonus;
    [Header("Events")] 
    public MoneyEvent playerPayOut;

    // npc event handler
    public float dropOffTimer;
    public float preferenceInterval;
    public float dialogueInterval;

    protected override void Enter()
    {
        playerCar = manager.objectiveTr.GetComponent<PlayerCar>();
        Debug.Log("npc has entered the taxi");
        setUpNpc();
        init();
    }

    private void setUpNpc() {
        Object.Destroy(transform.GetComponent<Rigidbody>());
        transform.position = playerCar.seat.position;
        transform.SetParent(manager.objectiveTr);
        playerCar.isPassenger = true;
    }

    private void init() {
        dialogueInterval = 10f;
        preferenceInterval = 10f;
        //dropOffTimer = Random.Range(180f,225f);
        dropOffTimer = 30f;
    }

   

    protected override void Tick()
    {
        EventsHandler();
    }
    Vector3 previousPos;
    private void EventsHandler() {  
        dropOffTimer -= Time.deltaTime;
        if (dropOffTimer <= 0) {EnterDropOff();}
        HandlePreferences();
        HandleDialogue();
    }

     private void EnterDropOff() {
        CalculatePlayerPay();
        manager.SwitchStates(new DropOffState());
    }

   

    private void HandleDialogue(){
         if (DialogueManager.GetInstance().isDialoguePlaying) { return;}
         if (dialogueInterval > 0) { 
               dialogueInterval -= Time.deltaTime;
                //Debug.Log(dialogueInterval);
                return;
            }
        //Debug.Log("provoke event");
        DialogueManager.GetInstance().EnterDialogue(manager.dialoguePool, "Test man");
       dialogueInterval = 15f;
    }

    private void HandlePreferences() {
        // interval
        if (preferenceInterval > 0) {
           preferenceInterval -= Time.deltaTime;
            return;
        }
        // check preference requirements
      tempBonus = CheckTemperature() ?tempBonus += 5 :tempBonus -= 5;
      stationBonus = CheckRadioStation() ?stationBonus += 5 :stationBonus -=5;
      windowsBonus = CheckWindows() ?windowsBonus += 5 :windowsBonus -= 5;
       
      tempBonus = Mathf.Clamp(tempBonus, 0,preferenceCap);
      stationBonus = Mathf.Clamp(stationBonus, 0,preferenceCap);
      windowsBonus =  Mathf.Clamp(windowsBonus, 0,preferenceCap);

        
      preferenceBonus = (tempBonus +stationBonus +windowsBonus);
      preferenceInterval = 10f;
    }

     bool CheckTemperature() {
         if (Mathf.Abs(manager.type.temperature - playerCar.Temperature.value) < 3f ) {
            return true;
        }
        return false;
    }
     bool CheckRadioStation() {
         if (playerCar.currentStation.value == manager.type.stationPreference) {
            return true;
        }
        return false;
    }
    
    bool CheckBools<T>(T val1, T val2) where T : class, new()
    {
        if (val1 == val2) {
            return true;
        }
        return false;
    }

     bool CheckWindows() {
        if (playerCar.WindowsOpen.value  == manager.type.prefersWindowsOpen) {
            return true;
        }
        return false;
    }



    public void AwardTherapyPoints() {
       therapyBonus = DialogueManager.GetInstance().dialogueScore;
       therapyBonus = Mathf.Clamp(therapyBonus, 0f, 100f);
       DialogueManager.GetInstance().clearDialogueScore();
    }

    private void CalculatePlayerPay() {
        float payOut;
        AwardTherapyPoints();
        Debug.Log("Therapy Bonus: " +therapyBonus * 15);
        Debug.Log("Preference Bonus: " +preferenceBonus);
        payOut =preferenceBonus + (therapyBonus * 15);
        Debug.Log("PayOut: " + payOut);
        manager.payOut = new transactionDataPack(payOut);
    }


    
}
