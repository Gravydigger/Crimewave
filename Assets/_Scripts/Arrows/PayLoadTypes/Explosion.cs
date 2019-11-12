using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script has a IPayload interface, controlled by Arrow.cs
public class Explosion : MonoBehaviour, IPayload
{
    Arrow arrow;

    public int explosiveDamage = 1;
    public float explosiveRadius = 3f;

    private void Start()
    {
        //Finds the base arrow script
        arrow = GetComponent<Arrow>();
    }

    //This gets called when the arrow collides with something
    public void Deliver()
    {
        //Create an explosion and damage all enemies in the radius
        CreateExplosion();

        //Show an explosion effect
        ExplosionSprite();
    }

    void CreateExplosion()
    {
        //sees if the colider is the hitbox (BoxCollider2D) and an enemy
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosiveRadius);

        foreach (Collider2D enemyInRange in hitColliders)
        {
            if (!enemyInRange.isTrigger)
            {
                EnemyManager currentEnemy = enemyInRange.GetComponent<EnemyManager>();
                if (currentEnemy != null)
                {
                    currentEnemy.TakeDamage(explosiveDamage, transform.position, arrow.firedFrom);
                }
            }
        }
    }

    void ExplosionSprite()
    {

    }

}
