using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents {
    // public events
    public delegate bool PurchaseObject(transactionDataPack transaction);
    public static PurchaseObject purchaseObject;

    public delegate void PlayerPayOut(transactionDataPack transaction);
    public static PlayerPayOut playerPayOut;

    
}