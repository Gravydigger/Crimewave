using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger instance;

    public Dialogue dialogue;

    private void Start()
    {
       instance = this;
    }

    public void TriggerDialouge()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
