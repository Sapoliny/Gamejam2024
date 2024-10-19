using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum state
{
    Dialogue,
    PlayerAttack,
    BossAttack
}

public class GameManagerTest : MonoBehaviour
{
    state currentState = state.Dialogue;
    state? lastState = null;

    public GameObject player;

    string dialogue;
    public TextMeshPro dialogueText;
    string[] dialogueLines; // To store the lines of dialogue
    int currentLineIndex = 0;
    public float typingSpeed = 0.05f; // Speed of the typing effect
    Coroutine typingCoroutine; // To keep track of the typing coroutine


    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<DialogueTest>().dialogue;
        dialogue = dialogue.Trim();
        dialogueLines = dialogue.Split(new[] { "\n" }, System.StringSplitOptions.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastState != currentState)
        {
            switch (lastState) //O que acontece ao sair de um estado
            {
                case state.Dialogue:
                    break;
                case state.PlayerAttack:
                    break;
                case state.BossAttack:
                    player.SendMessage("StopDefense");
                    break;
                default:
                    break;
            }

            switch (currentState) //O que acontece ao entrar num estado
            {
                case state.Dialogue:
                    if (dialogueLines.Length > 0)
                    {
                        ShowNextLine();
                    }
                    break;
                case state.PlayerAttack:
                    break;
                case state.BossAttack:
                    player.SendMessage("StartDefense");
                    break;
            }
        }

        lastState = currentState;

        switch (currentState) //O que acontece ao estar num estado
        {
            case state.Dialogue:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1)) // Press Space to advance the dialogue
                {
                    if (currentLineIndex < dialogueLines.Length)
                    {
                        ShowNextLine();
                    }
                    else
                    {
                        currentState = state.BossAttack;
                        if (typingCoroutine != null)
                        {
                            StopCoroutine(typingCoroutine); 
                        }
                        dialogueText.text = "";
                    }
                }
                break;
            case state.PlayerAttack:
                break;
            case state.BossAttack:
                break;
        }
    }

    void ShowNextLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop any previous typing coroutine
        }

        string currentLine = dialogueLines[currentLineIndex].Trim(); // Get the current line of dialogue and trim spaces

        // Check if the line starts with "P:" or "B:"
        if (currentLine.StartsWith("P:"))
        {
            // Move the TextMeshPro to the left (x = -3)
            dialogueText.transform.localPosition = new Vector3(-3, dialogueText.transform.localPosition.y, dialogueText.transform.localPosition.z);
        }
        else if (currentLine.StartsWith("B:"))
        {
            // Move the TextMeshPro to the right (x = 3)
            dialogueText.transform.localPosition = new Vector3(3, dialogueText.transform.localPosition.y, dialogueText.transform.localPosition.z);
        }

        // Update TextMeshPro with the current line of dialogue
        dialogueText.text = currentLine.Substring(2).Trim();

        typingCoroutine = StartCoroutine(TypeLine(currentLine));

        currentLineIndex++; // Move to the next line
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = ""; // Clear the text

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(typingSpeed); // Wait before displaying the next letter
        }
    }

}
