using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InMainMenu : PlayerState
{
    public override string intendedScene { get => "MainMenu"; protected set => base.intendedScene = "MainMenu"; }

    protected override void OnEnter()
    {
        Debug.Log("The player is in the Main Menu");
    }

    protected override void OnExit()
    {
        Debug.Log("The player is no longer in the Main Menu");
    }
}
