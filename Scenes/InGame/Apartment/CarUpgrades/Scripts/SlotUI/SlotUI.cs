using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Characteristic characteristic;
    // UI elements
    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI costDisplay;
    public Image imageDisplay;
    public Button purchaseButton;

   
    public void Upgrade() {
        characteristic.Upgrade();
        if (characteristic.Tier == characteristic.tiers.Length) {outOfStock(); return;}
        nameDisplay.text = characteristic.name + " " + characteristic.Tier.ToString();
        costDisplay.text = characteristic.tiers[characteristic.Tier].Cost.ToString();
    }

    private void outOfStock() {
        nameDisplay.text = "Maxxed";
        costDisplay.text = "???";
    }
    
    

    
}
