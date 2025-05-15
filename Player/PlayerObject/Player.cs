using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "player", menuName = "Game/Player")]
public class Player : ScriptableObject
{
    
    [Header("State")]
    public PlayerState currentState = new InTaxi();
    [Header("Attributes : Economics")]
    public float Money;
    public float dailyPayOut;
    [Header("Progress")]
    public int day;
    public float points;

    // Economics
    
}

