using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System;
using UnityEditor.TerrainTools;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class DialogueManager : MonoBehaviour
{

    [Header("Current Dialogue")]
    private Story currentStory;
    public bool isDialoguePlaying = false;
    public int dialogueScore;
    
   
    public delegate void OnDialogueEnter(String name);
    public static event OnDialogueEnter onDialogueEnter;

    public delegate void OnDialogueContinue(string promptString ,string[] choicesStrings);
    public static event OnDialogueContinue onDialogueContinue;

    public delegate void OnDialogueExit();
    public static event OnDialogueExit onDialogueExit;

    // handling singleton status
    public static DialogueManager instance;

    private void OnEnable() {
        PlayerUIManager.dialogueResponseSubmitted += onPlayerEnter;
    }

    private void OnDisable() {
        PlayerUIManager.dialogueResponseSubmitted -= onPlayerEnter;
    }

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Found More than one dialogue manager");
        }
        instance = this;
    }

    public static DialogueManager GetInstance() {
        return instance;
    }


    // set up UI status and defualt variables
    



    public void onPlayerEnter(string playerAnswer) {
        if (!isDialoguePlaying) { return;}
        bool responseMatches = false;
        Debug.Log("enter performed");
        // no dialogue choices, continue story...
        if (currentStory.currentChoices.Count == 0) { ContinueStory(); return;}

        // dialogue choices present, interpret player response...
        List<Choice> currentChoices = currentStory.currentChoices;
        int index = 0;
        foreach (Choice choice in currentChoices) {
            String choiceText = cleanString(choice.text);
            if (choiceText != playerAnswer) { continue;}
            Debug.Log("found matching response");
            index = currentChoices.IndexOf(choice);
            responseMatches = true;
         }
         
        // generate NPC response
        if (responseMatches) {
            currentStory.ChooseChoiceIndex(index);
            ContinueStory();
        }
        // generate alternative NPC response
        else { 
            DisplayDialogue("Uh, can you please  repeat yourself?");
        }
        
            
    }

    //  start dialogue (conversation)
    public void EnterDialogue(TextAsset inkJSON, string name) {
        if (isDialoguePlaying) { ExitDialogue();}
        Cursor.lockState = CursorLockMode.Confined;
        // set up current story
        currentStory = new Story(inkJSON.text);
        isDialoguePlaying = true;
        // set up dialogue
        onDialogueEnter?.Invoke(name);
        // start conversation
        ContinueStory();
    }



    // end dialogue
    private void ExitDialogue() {
        isDialoguePlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
        onDialogueExit?.Invoke();
    }

    private void ContinueStory() {
        if (currentStory.canContinue) {
            Debug.Log("displaying prompt text");
            // engage in dialogue
            currentStory.Continue();
            WeightChoices(); // weight previous choices
            //  display dialogue UI
            DisplayDialogue(currentStory.currentText);
            Debug.Log(currentStory.currentText);
        }
        else if (currentStory.currentChoices.Count == 0) {
            // end dialogue if no conversation routes left
            Debug.Log("exiting storry");
            ExitDialogue();
        }
    }

    private void DisplayDialogue(String promptString) {
        
        List<Choice> currentChoices = currentStory.currentChoices;
        String[] choiceStrings = new String[4];
        
        for (int i = 0; i < currentChoices.Count; i++) {
            choiceStrings[i] = currentChoices[i].text;
        }
        
        
        onDialogueContinue?.Invoke(promptString,choiceStrings);
    }

    

    

    private void WeightChoices() {
        List<String> currentPoints = currentStory.currentTags;
        Debug.Log(currentPoints);
        if (currentPoints.Count == 0) { return;}
            Debug.LogWarning("there are tags");
            int point;
            int.TryParse(currentPoints[0],out point);
            dialogueScore += point;
            // continue dialogue
                
            
    }

    public void clearDialogueScore() {
        dialogueScore = 0;
    }

    private String cleanString(String stringObj) {
        String newString;
        newString = stringObj.Replace(" ", string.Empty);
        newString = newString.ToLower();
        return newString;
    }

}
