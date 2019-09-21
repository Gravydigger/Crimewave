using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public EnemyMovement instance;

    EnemyManager EM;
    CharacterMovement CM;

    public SpriteRenderer flip;
    public Vector3 enemyPos;
    public Vector2 target;
    private bool isMoving = false;
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Start()
    {
        CM = CharacterMovement.instance;
    }

    private void Update()
    {
        EM = GetComponent<EnemyManager>();
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
    }

    //Makes the enemy move towards a target (will be replaced with pathfinding code)
    private void MoveEnemy(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, EM.enemySpeed * Time.deltaTime);
    }

    private void FlipEnemy()
    {
        if (rigidbody.velocity.x > 0 && !isMoving)
        {
            flip.flipX = true;
        }

        if (rigidbody.velocity.x < 0 && !isMoving)
        {
            flip.flipX = false;
        }

        else if (rigidbody.velocity.x == 0)
        {
            isMoving = false;
        }
    }
}
