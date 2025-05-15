using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingManager : MonoBehaviour
{
    [Header("Base Upgrades")]
    [SerializeField] Characteristic[] characteristics;
    [Header("UI")]
    public GameObject UpgradeUISlotTemplate;
    public GameObject BaseUpgradesUI;
    
    // create upgrade slots
    private void Start() {
    
        foreach (Characteristic characteristic in characteristics) {
           instaniateSlotUI(characteristic);
        }
    }

    // instantiate slot
    private void instaniateSlotUI(Characteristic characteristic) {
        // creation and list addition
        GameObject newUpgradeSlotUIObject = Instantiate(UpgradeUISlotTemplate, BaseUpgradesUI.transform);
        SlotUI newSlotUI = newUpgradeSlotUIObject.transform.GetComponent<SlotUI>();
        Debug.Log(newSlotUI);
        newSlotUI.purchaseButton.onClick.AddListener(() => onUpgradeClicked(newSlotUI));
        // setting slot's display properties
        newSlotUI.characteristic = characteristic;
        newSlotUI.nameDisplay.text = characteristic.name;
        if (characteristic.Tier == 0) {
            newSlotUI.costDisplay.text = characteristic.tiers[0].Cost.ToString();
        }
        else {
            newSlotUI.costDisplay.text = characteristic.tiers[characteristic.Tier - 1].Cost.ToString();
        }
       
    }

    // upgrade slot OnClick
    private void onUpgradeClicked(SlotUI slotUI) {
        if (slotUI.characteristic.tiers.Length == slotUI.characteristic.Tier) { return;}
        Debug.Log("slot UI clicked");
        transactionDataPack transaction = new transactionDataPack(Amount: slotUI.characteristic.tiers[slotUI.characteristic.Tier].Cost);
        bool success = PlayerEvents.purchaseObject.Invoke(transaction);
        if (!success) {return;}
        Debug.Log(slotUI.characteristic.name + " was upgraded...");
        slotUI.Upgrade();
    }

    
}
