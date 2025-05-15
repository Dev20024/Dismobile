using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager
{
    public static IEnumerator OnDayOver(PlayerManager playerManager) {
        Debug.Log("player manager picked up");
        // commit banking transaction
        Dictionary<string,float> FeesList = new Dictionary<string, float>() {
            {"Gas", 20f},
            {"Damages", 0f},
            {"Taxes", 0f},
        };
        transactionDataPack Fees = new transactionDataPack(0f,FeesList);
        EconomicsManager.CalculateFinalDailyPayOut(playerManager.playerData, Fees);

        
        // progress
        playerManager.playerData.day += 1;
        // apartment transition
        yield return new WaitForSeconds(5f);
        playerManager.onStateChange(new InApartment());
    }
    
    public static IEnumerator onDayStart(PlayerManager playerManager) {
        Debug.Log("Player Manager Picked Up");
        yield return new WaitForSeconds(5f);
        playerManager.onStateChange(new InTaxi());
    }


}
