using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public bool playerIsClose;
    private bool isTyping; // added variable to track whether text is typing out

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose && !isTyping) // disable player input if text is still typing out
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    NextLine();
                }
                else
                {
                    dialoguePanel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        isTyping = true; // set isTyping to true before typing out text

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false; // set isTyping to false after typing out text
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1 && !isTyping) // disable player input if text is still typing out
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else if (!isTyping)
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}