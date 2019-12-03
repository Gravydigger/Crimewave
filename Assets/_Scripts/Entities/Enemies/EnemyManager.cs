using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : EntityBase
{
    [HideInInspector] public EnemyManager instance;
    EnemyMovement EM;
    Renderer enemy;
    CharacterMovement CM;

    public static UnityEvent onAnyEnemyDeath = new UnityEvent();

    public int damageAmount = 1;
    public Status status;
    public float alertRadius = 5f;
    public float findPlayerDedication = 5f;
    public float knockbackDistance = 3f;
    private new Rigidbody2D rigidbody;

    [HideInInspector] public Vector2 gotHitFrom, playerLastSeen;

    public bool detectPlayer = false;
    public bool gotHit = false;

    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        EM = GetComponent<EnemyMovement>();
        enemy = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CM = CharacterMovement.instance;
    }

    //If player enters its visual arc, chase the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectPlayer = true;
        }
    }

    //if player exits its visual arc, move to where it last saw the player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLastSeen = CM.playerPosition;
            detectPlayer = false;
        }
    }
    
    //Deal damage to the player
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterManager CM = collision.gameObject.GetComponent<CharacterManager>();
            CM.TakeDamage(damageAmount, rigidbody);
            if (status != null)
                CM.ApplyStatus(status);
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
            ParticleSystem HitInstance = Instantiate(hurtParticle, transform.position, transform.rotation) as ParticleSystem;
            HitInstance.Play();
            Destroy(HitInstance.gameObject, HitInstance.main.duration + 0.1f);

            //Play a hit SFX
            hurtSound.Play();
        }

        //Knocks back the enemy
        KnockBack(arrowPos);

        //send where the arrow was fired from to EMV
        gotHitFrom = firedFrom;
        gotHit = true;

        //If enemy has 0 or negitive hp, call OnDeath()
        if (currentHealth <= 0 && !isDead)
            OnDeath();
    }

    //Knocks back the enemy in the opposite direction that they were hit
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

    public override void OnDeath()
    {
        //Tells game the enemy is dead, and makes sure it can't revive
        isDead = true;
        maxHealth = 0;

        //Fire off an event saying that an enemy has died
        onAnyEnemyDeath.Invoke();
        Debug.Log(gameObject.name + " has died.");

        //makes of the friends chase the player
        EM.AlertFriends(gotHitFrom);

        //Unparents the death particle effect, plays the particle effect & audio, then destroys it once it has finished
        deathParticle.transform.parent = null;
        deathParticle.Play();
        deathSound.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration + 0.1f);

        //Destroys the enemy gameObject
        Destroy(gameObject);
    }
}
