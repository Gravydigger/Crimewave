﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public ParticleSystem ps;
    public ParticleSystem explode;

    public float playerSpeed = 6f;
    [HideInInspector] public int maxHealth = 6;
    public int currentHealth = 6;
    [HideInInspector] public bool isDead = false;

    public Image[] hearts;

    [SerializeField] Sprite[] healthSprites;

    void Start()
    {
        instance = this;
        currentHealth = maxHealth;
        SetHealthUI();
        BleedAmount();
    }

    public void TakeDamage(int amount)
    {
        //Reduces current health by the amount of damage taken, and makes sure player is not overhealed
        currentHealth -= amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //Changes the UI elements appropriately 
        SetHealthUI();
        
        //Changes the bleed rate of player
        BleedAmount();

        //If player has 0 or negitive hp, call OnDeath()
        if (currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }
    }

    public void HealHealth(int amount)
    {
        //Increases current health by the amount of healing done, and makes sure player is not overhealed
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //Change the UI elements appropriately
        SetHealthUI();

        //Changes the bleed rate of player
        BleedAmount();
    }

    private void OnDeath()
    {
        //Tells game player is dead, and makes sure he can't revive
        isDead = true;
        maxHealth = 0;

        Debug.Log("Player is dead!");

        ParticleSystem PlayerExplodeInstance = Instantiate(explode, transform.position, Quaternion.identity) as ParticleSystem;

        //disables the player gameObject
        gameObject.SetActive(false);
    }

    private void SetHealthUI()
    {
        for (int i = 0; i < 3; i++)
        {
            // make a empty heart by default
            int index = 2;
            // make a full heart if health >= 6, 4  or 2 respectively
            if (currentHealth >= (6 - i * 2))
            {
                index = 0;
            }
            // make a half heart if health == 5, 3  or 1 respectively
            if (currentHealth == (5 - i * 2))
            {
                index = 1;
            }
            // set the correct sprite
            hearts[i].sprite = healthSprites[index];
        }
    }

    private void BleedAmount()
    {
        var bleed = ps.emission;
        //if player is at 0hp and is not at full health, make them bleed
        if (currentHealth != 0)
        {
            bleed.rateOverTime = Mathf.Abs(currentHealth - (maxHealth + 1)) / 2 * 1.5f;
        }

        //if player is at 0hp, stop the particle system.
        else
        {
            ps.Stop();
        }

        Debug.Log("Bleed Rate: " + Mathf.Abs(currentHealth - (maxHealth + 1)) / 2 * 1.5f);
    }
}
