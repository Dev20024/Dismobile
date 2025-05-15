using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "car upgrade tier", menuName = "Game/Car Upgrades/Tier")]
public class Tier : ScriptableObject
{
    [Header("Effects")]
    public float Cost;
    public float Buff;
    [Header("Visuals")]
    public GameObject modelUpgrade;
    public Texture icon;

    

}
