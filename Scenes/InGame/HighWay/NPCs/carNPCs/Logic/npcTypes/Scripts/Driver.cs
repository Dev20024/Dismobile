using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Driver", menuName = "npcType/Driver")]
public class Driver : ScriptableObject
{
    [Header("Attributes")]
    public GameObject carModelPool;
    [Header("Preferences")]
    [Range(1,8)]
    public int acceleration;
    [Range(1,10)]
    public int accelTime;
    [Range(-15,15)]
    public int speedDeviation;

}
