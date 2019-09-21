using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    CharacterManager CM;
    //EnemyManager EM;
    WeaponController WC;
    InputController IC;
    DialogueTrigger DT;
    DialogueManager DM;

    // Start is called before the first frame update
    private void Start()
    {
        CM = CharacterManager.instance;
        //EM = EnemyManager.instance;
        WC = WeaponController.instance;
        IC = InputController.instance;
        DT = DialogueTrigger.instance;
        DM = DialogueManager.instance;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.T))
                Hit();
            if (Input.GetKeyDown(KeyCode.Y))
                Heal();
            if (Input.GetKeyDown(KeyCode.U))
                Slay();
            if (Input.GetKeyDown(KeyCode.H))
                TriggerDialouge();
            if (Input.GetKeyDown(KeyCode.J))
                NextSentence();
        }
    }

    [ContextMenu("Player: Take 1 HP")]
    private void Hit()
    {
        CM.TakeDamage(1, null);
        Debug.Log("Player: Take 1 HP");
    }

    [ContextMenu("Player: Heal 1 HP")]
    private void Heal()
    {
        CM.HealHealth(1);
        Debug.Log("Player: Heal 1 HP");
    }

    [ContextMenu("Player: Slay Player")]
    private void Slay()
    {
        CM.currentHealth = 1;
        CM.TakeDamage(1, null);
        Debug.Log("Player: Slay Player");
    }

    private void TriggerDialouge()
    {
        DT.TriggerDialouge();
    }

    private void NextSentence()
    {
        DM.DisplayNextSentence();
    }
}
