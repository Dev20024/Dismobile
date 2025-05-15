using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]

public class PlayerManager : MonoBehaviour
{
    [Header("player")]
    public Player playerData;

    [Header("Components")]
    PlayerInputManager playerInputManager;

    // events
    public delegate void transactionEvent(transactionDataPack transaction);
    public static event transactionEvent displayDailyPayOut;

    

    private void OnEnable() {
        Debug.Log("player manager enabled");
        PlayerEvents.purchaseObject += makePurchase;
        PlayerEvents.playerPayOut += addToDailyPayOut;
    }

    private void OnDisable() {
        PlayerEvents.purchaseObject -= makePurchase;
        PlayerEvents.playerPayOut -= addToDailyPayOut;
    }
    
    private void Awake() {
        // get componenets
        playerInputManager = GetComponent<PlayerInputManager>();

        // setting current player state if null
        //if (playerData.currentState != null) {return;}
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentScene);
        if (currentScene == "Highway") {
            Debug.Log("current scene is highway");
            playerData.currentState = new InTaxi();
        }
        else if (currentScene == "Apartment") {
            Debug.Log("current scene is apartment");
            playerData.currentState = new InApartment();
        }
        Debug.Log(playerData.currentState);
        playerData.currentState.playerInputManager = playerInputManager;
        playerData.currentState.playerManager = this;
    }




    // state manager
    public void onStateChange(PlayerState newState) {
        Debug.Log("state changing");
        if (playerData.currentState != null) {
             playerData.currentState.OnStateExit();
        }
        playerData.currentState = newState;
        Debug.Log(playerData.currentState);
        playerData.currentState.OnStateEnter(this, playerInputManager);
    }

    private void Update() {
        playerData.currentState.OnUpdate();
        
       
        
    }

    // Day Manager
    public void onDayOver() {StartCoroutine(DayManager.OnDayOver(this));}
    public void onDayStart() { StartCoroutine(DayManager.OnDayOver(this));}

    // Economics Manager
    bool makePurchase(transactionDataPack transaction) =>  EconomicsManager.makePurchase(playerData, transaction);
    public void addToDailyPayOut(transactionDataPack transaction) => EconomicsManager.addToDailyPayOut(playerData, transaction);
    public void adjustFunds(transactionDataPack transaction) => EconomicsManager.adjustFunds(playerData,transaction);
    public void CalculateWeeklyRent() => EconomicsManager.CalculateWeeklyRent(playerData);
    
  
   


    
    
}



