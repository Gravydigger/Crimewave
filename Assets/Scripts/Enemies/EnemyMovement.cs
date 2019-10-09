using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public EnemyMovement instance;

    EnemyManager EM;
    CharacterMovement CM;
    Renderer enemy;

    SpriteRenderer flip;
    Animator animator;
    [HideInInspector] public Vector3 enemyPos;
    [HideInInspector] public Vector2 target;
    public Vector2 estimatedTarget;
    private new Rigidbody2D rigidbody;

    private int direction = 0;

    private void Awake()
    {
        EM = GetComponent<EnemyManager>();
        animator = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        instance = this;
        enemy = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CM = CharacterMovement.instance;
    }

    private void Update()
    {
        enemyPos = transform.position;

        DetectPlayer();

        FlipEnemy();

        MovingDirection();
    }

    void DetectPlayer()
    {
        //Moves the enemy in cardinal directions if it sees the player
        if (EM.detectPlayer || estimatedTarget != Vector2.zero)
        {
            if (EM.detectPlayer)
            {
                target = CM.playerPosition;
                MoveEnemy(target);
            }

            else
                MoveEnemy(estimatedTarget);
            
            if (EM.playerLastSeen != Vector2.zero || EM.gotHit)
            {
                EM.playerLastSeen = Vector2.zero;
                EM.gotHit = false;
            }

            if (Vector2.Distance(transform.position, estimatedTarget) < 0.01f)
            {
                estimatedTarget = Vector2.zero;
            }

        }

        //Moves to where it last saw the player
        else if (EM.playerLastSeen != Vector2.zero)
        {
            //go to where the player was lasts seen, and overshoot a certain distance
            target = EM.playerLastSeen;
            AlertFriends(target);
            float magnitude = target.magnitude;
            target.Normalize();
            target *= magnitude + EM.findPlayerOvershoot;

            MoveEnemy(target);
            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                EM.gotHit = false;
                EM.playerLastSeen = Vector2.zero;
            }
        }

        //Moves to where it was shot from if it cannot see the player currently
        else if (EM.gotHit)
        {
            target = EM.gotHitFrom;

            AlertFriends(target);
            MoveEnemy(target);
            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                EM.gotHit = false;

            }
        }
    }

    public void AlertFriends(Vector2 target)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, EM.alertRadius);

        foreach (Collider2D enemyInRange in hitColliders)
        {
            
            EnemyMovement currentEnemy = enemyInRange.GetComponent<EnemyMovement>();
            if (currentEnemy != null)
            {
                //float magnitude = target.magnitude;
                //target.Normalize();
                //target *= magnitude + EM.findPlayerOvershoot;
                //target += Random.insideUnitCircle;
                currentEnemy.estimatedTarget = target;
            }
        }
    }


    private void MovingDirection()
    {
        if (EM.detectPlayer)
        {
            //if moving left, give a negitive direction
            if (CM.playerPosition.x > transform.position.x)
            {
                direction = -1;
            }

            //if moving right, give positive direction
            if (CM.playerPosition.x < transform.position.x)
            {
                direction = 1;
            }
        }

        else
            direction = 0;

    }

    //Makes the enemy move towards a target (will be replaced with pathfinding code eventually)
    private void MoveEnemy(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, EM.enemySpeed * Time.deltaTime);
    }

    //Flips the enemy sprite depending if the enemy is stationary
    private void FlipEnemy()
    {
        if (direction < 0)
        {
            flip.flipX = false;
            animator.SetFloat("Speed", 1);
        }

        if (direction > 0)
        {
            flip.flipX = true;
            animator.SetFloat("Speed", 1);
        }

        else if (direction == 0)
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
