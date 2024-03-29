﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    CharacterManager CM;
    WeaponController WC;
    InputController IC;
    DialogueTrigger DT;
    DialogueManager DM;

    private void Start()
    {
        CM = CharacterManager.instance;
        WC = WeaponController.instance;
        IC = InputController.instance;
        DT = DialogueTrigger.instance;
        DM = DialogueManager.instance;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.J))
                Hit();
            if (Input.GetKeyDown(KeyCode.H))
                Heal();
            if (Input.GetKeyDown(KeyCode.K))
                KillPlayer();
            if (Input.GetKeyDown(KeyCode.L))
                KillAllEnemies();
            //if (Input.GetKeyDown(KeyCode.H))
            //TriggerDialouge();
            //if (Input.GetKeyDown(KeyCode.J))
            //NextSentence();
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
    private void KillPlayer()
    {
        CM.OnDeath();
        Debug.Log("Player: Slay Player");
    }

    [ContextMenu("Enemies: Kill All Enemies")]
    private void KillAllEnemies()
    {
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        foreach (EnemyManager enemy in enemies)
        {
            enemy.OnDeath();
        }
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
