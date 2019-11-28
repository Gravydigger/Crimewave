using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains the common components for both players and enemies such as hit points
public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public AudioSource hurtSound, deathSound;
    public ParticleSystem hurtParticle, deathParticle;

    // status effects currently affecting the character
    public List<Status> statusEffects;

    [HideInInspector] public bool isDead = false;

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;

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

        if (currentHealth <= 0 && !isDead)
            OnDeath();
    }

    public virtual void OnDeath() { }


    void Update()
    {
        //List of status' we will retire this frame because they have expired
        List<Status> deathRow = new List<Status>();

        //Update all of our statusEffects, marking those which will be retired
        foreach (Status status in statusEffects)
        {
            status.timer += Time.deltaTime;
            if (status.timer > status.duration)
                deathRow.Add(status);
            else
                status.UpdateStatus(this);
        }

        //Clean up expired ones now we've finished iterating over list
        foreach (Status status in deathRow)
        {
            //Do any special code for once it wears off, and remove from list
            status.RemoveStatus(this);
            statusEffects.Remove(status);
        }
    }

    public Status ApplyStatus(Status status)
    {
        Status inst = Instantiate(status);
        inst.name = status.name;
        statusEffects.Add(inst);
        inst.ApplyStatus(this);
        return inst;
    }
}
