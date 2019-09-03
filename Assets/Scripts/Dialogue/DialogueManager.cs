using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //https://forum.unity.com/threads/shaking-text.697982/
    public static DialogueManager instance;

    public TextMeshProUGUI dialougeText;

    private Queue<string> sentences;

    private void Start()
    {
        instance = this;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.nameIdentifier + " is starting a conversation");

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialougeText.text = sentence;
    }

    public void EndDialogue()
    {
        Debug.Log("Conversation ended.");
    }

}
