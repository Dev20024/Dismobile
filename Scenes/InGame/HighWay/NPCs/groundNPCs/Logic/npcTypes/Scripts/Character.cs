using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "npcType/Character")]
public class Character : ScriptableObject
{
    [Header("Attributes")]
    public Species species;
    public GameObject characterModel;
    public List<string> NamePool = new List<string>();
    
    [Header("Dialogue")]
    public TextAsset dialoguePool;

    [Header("Preferences")]
    [Range(55f, 90f)]
    public float temperature; 
    public RadioStation stationPreference;
    public bool prefersWindowsOpen;


}

public enum Species {
    test,
    snake, 
    wolf
}
 


