using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    // In order to not check for constant input from the space bar when not in dialogue, we set this variable to false by default
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors) { 
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started conversation! Loaded messages: " + messages.Length);
        DisplayMessage();
    }

    //Gets the messages to be displayed
    void DisplayMessage() {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage() {
        // Increases the message id integer and checks if there are more messages saved. If not changes message box to inactive and ends the dialogue
        // for the space bar input
        activeMessage++;
        if (activeMessage < currentMessages.Length) { 
            DisplayMessage();
        } else {
            Debug.Log("Conversation ended");
            isActive = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the conversation is active and the player presses the space button. If true loads next message
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true) { 
            NextMessage();
        }
    }
}
