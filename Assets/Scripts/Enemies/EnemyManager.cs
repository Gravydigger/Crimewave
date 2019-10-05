using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public EnemyManager instance;
    //CharacterManager CM;
    EnemyMovement EM;

    public static UnityEvent onAnyEnemyDeath = new UnityEvent();

    public ParticleSystem enemyHurtParticle;
    public ParticleSystem enemyDeathParticle;

    public AudioSource enemyHurtSound;
    public AudioSource enemyDeathSound;

    public float enemySpeed = 6f;
    public int maxHealth = 2;
    public int currentHealth = 1;
    public int damageAmount = 1;
    public float knockbackDistance = 3f;
    private new Rigidbody2D rigidbody;

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

        //If the enemy is not dead...
        if (currentHealth > 0 && !isDead)
        {
            //Spawn hit particle FX 
            ParticleSystem HitInstance = Instantiate(enemyHurtParticle, transform.position, transform.rotation) as ParticleSystem;
            HitInstance.Play();
            Destroy(HitInstance.gameObject, HitInstance.main.duration + 0.1f);

            //Play a hit SFX
            enemyHurtSound.Play();
        }

        //Knocks back the enemy
        KnockBack(arrowPos);

        //send where the arrow was fired from to EMV
        gotHitFrom = firedFrom;

        //If enemy has 0 or negitive hp, call OnDeath()
        if (currentHealth <= 0 && !isDead)
            OnDeath();
    }

    //Knocks back the enemy in the oppostie direction that they were hit
    private void KnockBack(Vector3 arrowPos)
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

    public void OnDeath()
    {
        //Tells game the enemy is dead, and makes sure it can't revive
        isDead = true;
        maxHealth = 0;

        //Fire off an event saying that an enemy has died
        onAnyEnemyDeath.Invoke();

        Debug.Log(gameObject.name + " has died.");

        //Unparents the death particle effect, plays the particle effect & audio, then destroys it once it has finished
        enemyDeathParticle.transform.parent = null;
        enemyDeathParticle.Play();
        enemyDeathSound.Play();
        Destroy(enemyDeathParticle.gameObject, enemyDeathParticle.main.duration + 0.1f);

        //Destroys the enemy gameObject
        Destroy(gameObject);
    }
}
