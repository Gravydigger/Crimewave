using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public EnemyMovement instance;

    EnemyManager EM;
    CharacterMovement CM;

    SpriteRenderer flip;
    Animator animator;
    [HideInInspector] public Vector3 enemyPos;
    [HideInInspector] public Vector2 target;
    private new Rigidbody2D rigidbody;

    private Vector2 oldPos;
    private int direction = 0;

    private void Awake()
    {
        EM = GetComponent<EnemyManager>();
        animator = GetComponent<Animator>();
        flip = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        instance = this;
        oldPos = transform.position;
    }

    private void Start()
    {
        CM = CharacterMovement.instance;
    }

    private void Update()
    {
        //EM = GetComponent<EnemyManager>();
        //Moves the enemy in cardinal directions if it sees the player
        if (EM.detectPlayer)
        {
            target = CM.playerPosition;
            MoveEnemy(target);
            EM.gotHitFrom = Vector2.zero;
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

        enemyPos = transform.position;

        //Flips the enemy sprite depending if the enemy is stationary
        FlipEnemy();

        isMoving();
    }

    private void isMoving()
    {
        //EM = GetComponent<EnemyManager>();
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

        oldPos = transform.position;
    }

    //Makes the enemy move towards a target (will be replaced with pathfinding code)
    private void MoveEnemy(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, EM.enemySpeed * Time.deltaTime);
        //rigidbody.MovePosition(target + transform.position * Time.fixedDeltaTime);
    }

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
