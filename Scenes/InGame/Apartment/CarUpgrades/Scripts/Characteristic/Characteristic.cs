using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "Characteristic", menuName = "Game/Car Upgrades/Characteristic")]
public class Characteristic : ScriptableObject
{
    public string Name;
    public float BaseStat;
    public float Stat;
    public int Tier = 0;
    public Tier[] tiers;
    public Tier currentTier;
    
    public void Upgrade() {
        if (Tier == 3) {return;}
        Tier++;
        currentTier = tiers[Tier - 1];
        Stat = BaseStat * (1+(currentTier.Buff * .01f));
        if (currentTier.modelUpgrade == null) { return;}
        ApplyModeUpgrade();
    }


    private void ApplyModeUpgrade() {

    }
}
