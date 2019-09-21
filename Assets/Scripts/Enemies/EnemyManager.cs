using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public EnemyManager instance;
    //CharacterManager CM;
    EnemyMovement EM;

    public ParticleSystem enemyHitEffect;
    public ParticleSystem explode;

    public AudioSource enemyHurt;

    public float enemySpeed = 6f;
    public int maxHealth = 2;
    public int currentHealth = 1;
    public int damageAmount = 1;
    public float knockbackDistance = 3f;
    public new Rigidbody2D rigidbody;

    [HideInInspector] public Vector2 gotHitFrom;

    public bool detectPlayer = false;
    [HideInInspector] public bool isDead = false;

    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        EM = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        //CM = CharacterManager.instance;
    }

    //If player enters its visual arc, chase player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectPlayer = true;
            //Debug.Log("Player Detected.");
        }
    }
    
    //if player exits its visual arc, move to where it last saw the player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectPlayer = false;
            //Debug.Log("Player Lost.");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterManager CM = collision.gameObject.GetComponent<CharacterManager>();
            CM.TakeDamage(damageAmount, rigidbody);
        }
    }

    public void TakeDamage(int amount, Vector2 arrowPos, Vector2 firedFrom)
    {
        //Makes sure the player is not overhealed
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        //reduces current health by the amount of damage taken
        currentHealth -= amount;

        //Knocks back the enemy
        KnockBack(arrowPos);

        //send where the arrow was fired from to EMV
        gotHitFrom = firedFrom;

        //Emits a hit particle effect if the enemy is not dead
        if (currentHealth > 0 && !isDead)
        {
            ParticleSystem HitInstance = Instantiate(enemyHitEffect, transform.position, transform.rotation) as ParticleSystem;
            HitInstance.Play();
            Destroy(HitInstance.gameObject, HitInstance.main.duration + 0.1f);
        }

        //play hit sound
        enemyHurt.Play();

        //If enemy has 0 or negitive hp, call OnDeath()
        if (currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }
    }

    public void KnockBack(Vector3 arrowPos)
    {
        Vector2 knockbackDirection = EM.enemyPos - arrowPos;
        knockbackDirection.Normalize();
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(knockbackDirection * knockbackDistance, ForceMode2D.Impulse);
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

        Debug.Log(gameObject.name + " has died.");

        //unparents the explode particle effect, plays the particle effect, then destroys it once it has finished
        explode.transform.parent = null;
        explode.Play();
        Destroy(explode.gameObject, explode.main.duration + 0.1f);

        //destroys the enemy gameObject
        Destroy(gameObject);
    }
}
