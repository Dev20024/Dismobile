using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PlayerState {

    public PlayerManager playerManager;
    public PlayerInputManager playerInputManager;
    public virtual String intendedScene {get; protected set;}

    public void OnStateEnter(PlayerManager playerManager, PlayerInputManager playerInputManager) {
        this.playerManager = playerManager;
        this.playerInputManager = playerInputManager;
        Debug.Log(intendedScene);
        loadIntendedScene();
        OnEnter();
    }

    protected virtual void OnEnter() {Debug.Log("entering state");}

    public void OnStateExit() {
        OnExit();
    }

    protected virtual void OnExit() {Debug.Log("exiting state");}

    public virtual void OnUpdate() {}

    private void loadIntendedScene() {
        if (intendedScene != null) {
            if (SceneManager.GetActiveScene().name != intendedScene) {
                Debug.Log("loading " + intendedScene);
                SceneManager.LoadScene(intendedScene);
            }
        }
    }
}
