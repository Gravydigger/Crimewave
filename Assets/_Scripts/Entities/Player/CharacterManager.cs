﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterManager : EntityBase
{
    public static CharacterManager instance;
    public static UnityEvent onPlayerDeath = new UnityEvent();

    CharacterMovement CM;

    public ParticleSystem bleed;

    public float invincibilityDuration = 1f;
    public float invincibilityDelay = 0f;
    public bool isInvincible = false;

    public float knockbackDistance = 3f;
    new Rigidbody2D rigidbody;

    public Image[] hearts;

    [SerializeField] Sprite[] healthSprites;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        instance = this;
        playerSprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth = 6;
        SetHealthUI();
        BleedAmount();
    }

    void Start()
    {
        CM = CharacterMovement.instance;
    }

    private void Update()
    {
        if (isInvincible)
            Invincibility();

        if (invincibilityDelay > 0)
        {
            playerSprite.enabled = Mathf.Sin(invincibilityDelay * 30f) < 0;
        }
        else
        {
            playerSprite.enabled = true;
        }
    }

    public void TakeDamage(int amount, Rigidbody2D enemyRigidbody)
    {
        //If the player in invincible, don't deal dmg
        if (isInvincible)
            return;

        //Shows what enemy hit the player
        EnemyMovement EM = enemyRigidbody.GetComponent<EnemyMovement>();

        //Makes sure the player is not overhealed
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //reduces current health by the amount of damage taken
        currentHealth -= amount;

        //Knocks the player back a distance, provided an enemy hits them
        if (EM != null)
        {
            KnockBack(EM);
        }   

        isInvincible = true;
        //Changes the UI elements appropriately 
        SetHealthUI();
        
        //Changes the bleed rate of player and emmits a hit particle effect
        BleedAmount();

        //If the player is not dead
        if (currentHealth > 0 && !isDead)
        {
            //Spawn hit particle FX
            ParticleSystem HurtInstance = Instantiate(hurtParticle, transform.position, transform.rotation) as ParticleSystem;
            HurtInstance.Play();
            Destroy(HurtInstance.gameObject, HurtInstance.main.duration + 0.1f);

            //Play hit SFX
            hurtSound.Play();
        }

        //If player has 0 or negitive hp, call OnDeath()
        if (currentHealth <= 0 && !isDead)
            OnDeath();
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

    private void Invincibility()
    {
        //shows how long the player is invincible
        invincibilityDelay += Time.deltaTime;
        if (invincibilityDelay > invincibilityDuration)
        {
            isInvincible = false;
            invincibilityDelay = 0f;
        }
    }

    //Knocks back the player in the oppostie direction that they were hit
    private void KnockBack(EnemyMovement EM)
    {   
        Vector2 knockbackDirection = CM.playerPosition - EM.enemyPos;
        knockbackDirection.Normalize();
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(knockbackDirection * knockbackDistance, ForceMode2D.Impulse);
    }

    public override void OnDeath()
    {
        //Tells game player is dead, and makes sure he can't revive
        isDead = true;
        isInvincible = false;
        maxHealth = 0;
        currentHealth = 0;

        //Change the UI elements appropriately
        SetHealthUI();

        //Fire off an event saying that the Player has died
        onPlayerDeath.Invoke();

        Debug.Log("Player is dead!");

        //Unparents the death particle effect, plays the particle effect & audio, then destroys it once it has finished
        deathParticle.transform.parent = null;
        deathParticle.Play();
        deathSound.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration + 0.1f);

        //Disables the player gameObject
        gameObject.SetActive(false);
    }

    private void SetHealthUI()
    {
        for (int i = 0; i < 3; i++)
        {
            //Make a empty heart by default
            int index = 2;
            //Make a full heart if health >= 6, 4  or 2 respectively
            if (currentHealth >= (6 - i * 2))
            {
                index = 0;
            }
            //Make a half heart if health == 5, 3  or 1 respectively
            if (currentHealth == (5 - i * 2))
            {
                index = 1;
            }
            //Set the correct sprite
            hearts[i].sprite = healthSprites[index];
        }
    }

    private void BleedAmount()
    {
        var bleedRate = bleed.emission;
        //If player is at 0hp and is not at full health, make them bleed
        if (currentHealth != 0)
        {
            bleedRate.rateOverTime = Mathf.Abs(currentHealth - (maxHealth + 1)) / 2 * 1.5f;
        }

        //If player is at 0hp, stop the particle system.
        else
        {
            bleed.Stop();
        }
    }
}
