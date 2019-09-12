using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTimer : MonoBehaviour
{
    DialogueManager DM;
    Dialogue dialogue;
    Image dialougeTimer;
    bool finishedSentence = false;

    float timer = 1f;

    void Start()
    {

        DM = DialogueManager.instance;
        dialougeTimer = GetComponent<Image>();
    }

    void Restart()
    {
        dialougeTimer.fillAmount = 1f;
    }

    void Update()
    {
        //wait for the person to finish talking (or if the player makes person say the whole conversation)
        //if person has finished talking, activate the timer with the duration set to the sentence.
        //if timer reaches 0 or player skips sentence, move to next sentcene, reset the timer, and set the duration to the timer of the next sentence.


        //if sentence has finished, start timer
        if (finishedSentence)
        {
            SentenceTimer();
        }
    }

    public void SentenceTimer()
    {
        float timerFill = 1 - (timer / (dialogue.sentenceDisplayTime[1] * 1000f));
        dialougeTimer.fillAmount = timerFill;
        timer += Time.deltaTime;
    }

}
