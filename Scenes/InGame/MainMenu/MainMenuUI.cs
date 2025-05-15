using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] PlayerStateEvent changeState;

    public void onPlayButton() {
        changeState.Fire(new InTaxi());
    }
}
