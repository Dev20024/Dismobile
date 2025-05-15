using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Canvas mainCanvas;

    // mouse UI
    [Header("Mouse UI")]
    [SerializeField] GameObject activeMouse;
    [SerializeField] GameObject passiveMouse;
    private bool mouseActive;
    // DialogueUI
    [Header("Dialogue UI")]
    [SerializeField] GameObject DialogueUI;
    [Header("NPC Speech")]
    [SerializeField] TextMeshProUGUI promptDisplay;
    [SerializeField] TextMeshProUGUI nameDisplay;
    [SerializeField] RawImage faceDisplay;
    [SerializeField] TextMeshProUGUI Directions;
    [Header("Player Speech")]
    [SerializeField] GameObject[] choicesGameObjects;
    private TextMeshProUGUI[] choicesText;
    [SerializeField] GameObject playerResponse;
    TMP_InputField playerResponseUI;

    // events
    public delegate void DialogueResponseSubmitted(string responseString);
    public static event DialogueResponseSubmitted dialogueResponseSubmitted;
    
    // setting up events
    private void OnEnable() {
        DialogueManager.onDialogueEnter += EnterDialogueUI;
        DialogueManager.onDialogueContinue += DisplayDialogue;
        DialogueManager.onDialogueExit += CleanUpDialogue;
        
    }

    private void OnDisable() {
        DialogueManager.onDialogueEnter -= EnterDialogueUI;
        DialogueManager.onDialogueContinue -= DisplayDialogue;
        DialogueManager.onDialogueExit -= CleanUpDialogue;
    }

    private void Start() {
        // setting up dialogue UI
        DialogueUI.SetActive(false);
        playerResponse.SetActive(false);
        // response choices
        choicesText = new TextMeshProUGUI[choicesGameObjects.Length];
        int index = 0;
        foreach (GameObject choice in choicesGameObjects) {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        // response input
        playerResponseUI = playerResponse.GetComponent<TMP_InputField>();
    }

    private void Update() {
        // mouse movement
        passiveMouse.transform.position = Mouse.current.position.ReadValue();
        activeMouse.transform.position = Mouse.current.position.ReadValue();
        
    }



    // Dialogue UI functionality

    public void EnterDialogueUI(string name) {
        DialogueUI.SetActive(true);
        nameDisplay.text = name;
    }

    public void DisplayDialogue(String currentPrompt ,String[] currentChoices) {
        CleanUpDialogue();
        DialogueUI.SetActive(true);
        // display prompt
        promptDisplay.text = currentPrompt;
        Debug.Log("prompt successfully displayed");

        if (currentChoices[0] == "") {return;}
        // dialogue choices present
        playerResponse.SetActive(true);
        // enabling used choice textBoxes
        int index = 0;
        foreach (String choice in currentChoices) {
            choicesGameObjects[index].gameObject.SetActive(true);
            choicesText[index].text = choice;
            index++;
        }
        // disabling un-used choice textBoxes
        for  (int i = index; i < choicesGameObjects.Length; i++) {
            choicesGameObjects[index].gameObject.SetActive(false);
        }
    }

    public void ResponseDialogue(InputAction.CallbackContext ctx) {
        if (!ctx.performed) {return;}
        String responseString = cleanString(playerResponseUI.text);
        dialogueResponseSubmitted?.Invoke(responseString);
    }

    public void CleanUpDialogue() {
        DialogueUI.SetActive(false);
        // clear UI text
        promptDisplay.text = "";
        nameDisplay.text = "";
        // clear dialogue choices
        clearDialogueChoices();
    }

    private void clearDialogueChoices() {
        foreach (TextMeshProUGUI choiceOption in choicesText) {
            choiceOption.text = "";
        }
    }




    // mouse functionality
    public void cursorEvent() {
        mouseActive = !mouseActive;

        if (mouseActive) {
            activeMouse.SetActive(true);
            passiveMouse.SetActive(false);
        }
        else {
            activeMouse.SetActive(false);
            passiveMouse.SetActive(true);
        }
    }


    // utility functions
    private String cleanString(String stringObj) {
        String newString;
        newString = stringObj.Replace(" ", string.Empty);
        newString = newString.ToLower();
        return newString;
    }

}
