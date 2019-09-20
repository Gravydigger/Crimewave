using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    CharacterManager CM;

    public ParticleSystem enemyHitEffect;
    public ParticleSystem explode;

    public AudioSource enemyHurt;

    public float enemySpeed = 6f;
    public int maxHealth = 2;
    public int currentHealth = 1;
    public int damageAmount = 1;
    public bool detectPlayer = false;
    [HideInInspector] public bool isDead = false;

    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
    }

    void Start()
    {
        CM = CharacterManager.instance;

    }

    //If player enters its visual arc, chase player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectPlayer = true;
            Debug.Log("Player Detected.");
        }
    }
    
    //if player exits its visual arc, move to where it last saw the player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectPlayer = false;
            Debug.Log("Player Lost.");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CM.TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(int amount)
    {
        //Reduces current health by the amount of damage taken, and makes sure the enemy is not overhealed
        currentHealth -= amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //Emits a hit particle effect
        enemyHitEffect.Play();

        //play hit sound
        enemyHurt.Play();

        //If enemy has 0 or negitive hp, call OnDeath()
        if (currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }
    }

    public void HealHealth(int amount)
    {
        //Increases current health by the amount of healing done, and makes sure enemy is not overhealed
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void OnDeath()
    {
        //Tells game the enemy is dead, and makes sure it can't revive
        isDead = true;
        maxHealth = 0;

        Debug.Log("Enemy has died.");

        ParticleSystem EnemyExplodeInstance = Instantiate(explode, transform.position, Quaternion.identity) as ParticleSystem;

        //disables the enemy gameObject
        gameObject.SetActive(false);
    }
}
