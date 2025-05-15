using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using JetBrains.Annotations;
using UnityEngine;

public class EconomicsManager
{
    // Shopping
    public static bool makePurchase(Player playerData, transactionDataPack purchase) {
        if ((playerData.Money - purchase.Amount) < 0)  {return false;}
        playerData.Money -= purchase.Amount;
        Debug.Log("purchase made, $" + purchase.Amount + " was deducted from account");
        return true;
    }

    // daily economics
    public delegate void OnDailyPayOut();
    public static OnDailyPayOut onDailyPayOut;

    public static void addToDailyPayOut(Player playerData, transactionDataPack transaction) {
        playerData.dailyPayOut += transaction.Amount;
    }

    public static void adjustFunds(Player playerData, transactionDataPack transaction) {
        playerData.Money += transaction.Amount;
        if (playerData.Money < 0f) {
            // bankrupt
        }
    }
    
    public static void CalculateFinalDailyPayOut(Player playerData, transactionDataPack Fees) {
        onDailyPayOut?.Invoke();
        // deduct fees
        playerData.dailyPayOut += Fees.Amount;
        // add daily payout to balance
        playerData.Money += playerData.dailyPayOut;
        playerData.dailyPayOut = 0f;
    }

    public static void CalculateWeeklyRent(Player playerData) {

    }
}

public struct transactionDataPack {
    public float PreAmount;
    public Dictionary<string, float> Fees;
    public float Amount;
    // standard transaction with Fees
    public transactionDataPack(float PreAmount, Dictionary<string,float> Fees) {
        this.PreAmount =  PreAmount;
        this.Fees = Fees;
        this.Amount = PreAmount;

        foreach(float Fee in this.Fees.Values) {
            Amount -= Fee;
        }
    }
    // standard transaction with no fees
    public transactionDataPack(float Amount){
        this.PreAmount = Amount;
        this.Amount = Amount;
        this.Fees = null;
    }
    
    // Refresh transaction pack
    public void Refresh() {
        this.Amount = this.PreAmount;
        foreach(float Fee in this.Fees.Values) {
            Amount -= Fee;
        }
    }
}