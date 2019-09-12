using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //https://forum.unity.com/threads/shaking-text.697982/
    public static DialogueManager instance;
    InputController IC;

    public TextMeshProUGUI dialougeText;

    private Queue<string> sentences;
    //private Queue<float> displayTime;

    private void Start()
    {
        IC = InputController.instance;
        instance = this;
        sentences = new Queue<string>();
        //displayTime = new Queue<float>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.nameIdentifier + " is starting a conversation");

        sentences.Clear();
        //displayTime.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            
        }
        //foreach (float displayTime in dialogue.sentenceDisplayTime)
        //{
            
        //}

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
        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(sentences));

        dialougeText.text = sentence;
    }

    /*IEnumerable TypeSentence (string sentence)
    {
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return null;
        }
    }*/

    public void EndDialogue()
    {
        Debug.Log("Conversation ended.");
    }

}
