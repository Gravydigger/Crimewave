using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;

    public float playerSpeed = 6f;
    [HideInInspector] public int maxHealth = 6;
    public int currentHealth = 6;
    [HideInInspector] public bool isDead = false;

    [SerializeField] Sprite[] healthSprites;

    void Start()
    {
        instance = this;
        currentHealth = maxHealth;
    }

    private void SetHealthUI()
    {
        ////////
        //TODO//
        ////////
    }

    private void TakeDamage(int amount)
    {
        //Reduces current health by the amount of damage taken, and makes sure player is not overhealed
        currentHealth -= amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //Changes the UI elements appropriately 
        SetHealthUI();

        //If player has 0 or negitive hp, call OnDeath()
        if (currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }
    }

    private void HealHealth(int amount)
    {
        //Increases current health by the amount of healing done, and makes sure player is not overhealed
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //Change the UI elements appropriately
        SetHealthUI();
    }

    private void OnDeath()
    {
        ////////
        //TODO//
        ////////
    }

}
