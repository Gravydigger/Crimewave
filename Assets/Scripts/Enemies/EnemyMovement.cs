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
    private new Rigidbody2D rigidbody;

    private Vector3 oldPos;
    private int direction = 0;

    private void Awake()
    {
        EM = GetComponent<EnemyManager>();
        animator = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        instance = this;
        oldPos = transform.position;
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

        IsMoving();
    }

    void DetectPlayer()
    {
        
        if (EM.detectPlayer >= 0 && EM.detectPlayer <= EM.wakeUpTime)
            EM.detectPlayer += Time.deltaTime;

        //Moves the enemy in cardinal directions if it sees the player
        if (EM.detectPlayer >= EM.wakeUpTime)
        {
            if (enemy.isVisible)
            {
                target = CM.playerPosition;
            }
            MoveEnemy(target);
            EM.playerLastSeen = Vector2.zero;
            EM.gotHitFrom = Vector2.zero;
        }

        //Moves to where it last saw the player
        else if (EM.playerLastSeen != Vector2.zero)
        {
            //go to where the player was lasts seen, and overshoot a certain distance
            target = EM.playerLastSeen;
            float magnitude = target.magnitude;
            target.Normalize();
            target *= magnitude + EM.findPlayerOvershoot;

            MoveEnemy(target);
            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                EM.gotHitFrom = Vector2.zero;
            }
        }

        //Moves to where it was shot from if It cannot see the player currently
        else if (EM.gotHitFrom != Vector2.zero)
        {
            target = EM.gotHitFrom;
            MoveEnemy(target);
            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                EM.gotHitFrom = Vector2.zero;
            }
        }
    }

    private void MovingDirection()
    {
        if (EM.detectPlayer > 2f)
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

    bool IsMoving()
    {
        if (transform.position == oldPos)
        {
            return false;
        }

        oldPos = transform.position;
        return true;
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
