﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement instance;

    //InputController IC;
    EnemyManager EM;
    CharacterManager CM;

    private bool isMoving = false;
    public SpriteRenderer flip;
    public Vector3 enemyPos;

    private void Start()
    {
        instance = this;
        //IC = InputController.instance;
        EM = EnemyManager.instance;
        CM = CharacterManager.instance;
    }

    private void Update()
    {
        //Moves the enemy in cardinal directions if it sees the player
        if (EM.detectPlayer)
        {
            MoveEnemy();
        }

        enemyPos = transform.position;
        //Flips the enemy sprite depending if the enemy is stationary
        //FlipEnemy();
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, CM.playerPosition, EM.enemySpeed * Time.deltaTime);
    }

    /*private void FlipEnemy()
    {
        

        if (IC.mouseXCoord >= 0 && !isMoving)
        {
            flip.flipX = false;
        }

        if (IC.mouseXCoord < 0 && !isMoving)
        {
            flip.flipX = true;
        }

        else
        {
            isMoving = false;
        }
    }*/
}
