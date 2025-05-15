using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StationDisplay : MonoBehaviour
{
    [SerializeField] StationVariable radioStation;
    [SerializeField] TextMeshProUGUI displayPort;

    private void Update() {
        displayPort.text = radioStation.value.ToString();
    }
}
