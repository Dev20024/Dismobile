using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Biome", menuName = "Game/Biome")]
public class Biome : ScriptableObject
{
    [Header("Infastructure")]
    public GameObject[] roads;
    public GameObject[] buildings;
    [Header("NPCS")]
    public SpawnChances_GroundNPC walkingNPCSpawnChances;
    [Header("Properties")]
    public float rarity;
    public int length;
}
