using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new spawn chances list", menuName = "Game/NPC/GroundNPC/SpawnChances")]
public class SpawnChances_GroundNPC : ScriptableObject
{
    public WeightCharacterPair[] spawnChances;
}
