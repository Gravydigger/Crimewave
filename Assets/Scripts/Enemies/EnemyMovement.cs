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
    [HideInInspector] public Vector2 target, estimatedTarget;
    private new Rigidbody2D rigidbody;

    private int direction = 0;
    private bool friendsPinged = false;

    private void Awake()
    {
        EM = GetComponent<EnemyManager>();
        animator = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        enemy = GetComponent<SpriteRenderer>();
        instance = this;
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
        //Moves the enemy if it sees the player
        if (EM.detectPlayer || estimatedTarget != Vector2.zero)
        {
            //If it can see the player...
            if (EM.detectPlayer)
            {
                //Move towards player
                target = CM.playerPosition;
                MoveEnemy(target);

                //If it already hasn't, tell its friends
                if (!friendsPinged)
                    AlertFriends(target);
                friendsPinged = true;
            }

            //go to where it friends told it
            else if (estimatedTarget != Vector2.zero)
                MoveEnemy(estimatedTarget);
            
            //reset other detection methods
            if (EM.playerLastSeen != Vector2.zero || EM.gotHit)
            {
                EM.playerLastSeen = Vector2.zero;
                EM.gotHit = false;
            }

            //When it reaches it's destination, stand still
            if (Vector2.Distance(transform.position, estimatedTarget) < 0.01f)
            {
                estimatedTarget = Vector2.zero;
            }
        }

        //Moves to where it last saw the player
        else if (EM.playerLastSeen != Vector2.zero)
        {
            //go to where the player was lasts seen, and overshoot a certain distance (and alert friends)
            target = EM.playerLastSeen;
            AlertFriends(target);
            float magnitude = target.magnitude;
            target.Normalize();
            target *= magnitude + EM.findPlayerDedication;
            target += Random.insideUnitCircle / 2f;
            MoveEnemy(target);

            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                EM.playerLastSeen = Vector2.zero;
            }
        }

        //Moves to where it was shot from
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
            if (!enemyInRange.isTrigger)
            {
                EnemyMovement currentEnemy = enemyInRange.GetComponent<EnemyMovement>();
                if (currentEnemy != null)
                {
                    float magnitude = target.magnitude;
                    target.Normalize();
                    target *= magnitude + EM.findPlayerDedication;
                    target += Random.insideUnitCircle / 2f;
                    currentEnemy.estimatedTarget = target;
                }
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
