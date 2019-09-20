using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement instance;

    //InputController IC;
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
        //IC = InputController.instance;
        EM = EnemyManager.instance;
        CM = CharacterMovement.instance;
    }

    private void Update()
    {
        //Moves the enemy in cardinal directions if it sees the player
        if (EM.detectPlayer)
        {
            target = CM.playerPosition;
            MoveEnemy(target);
        }

        enemyPos = transform.position;
        //Flips the enemy sprite depending if the enemy is stationary
        FlipEnemy();
    }

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
