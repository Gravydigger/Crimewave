using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance;

    GameManager GM;
    InputController IC;
    public Animator animator;

    public Vector3 playerPosition;
    public float playerSpeed = 6f;
    private bool isMoving = false;
    public SpriteRenderer flip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        IC = InputController.instance;
        GM = GameManager.instance;
    }

    private void Update()
    {
        playerPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!GM.gameOver)
        {
        //Moves the character in cardinal directions
        MoveCharacter();
        //Flips the player sprite depending if the player is stationary
        FlipCharacter();
        //controls the animator to spawn between standing and running
        Animator();
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

    }

    private void MoveCharacter()
    {
        /***********************Moving the Character***********************/
        if (IC.moveLeft)
        {
            transform.Translate(-playerSpeed * Time.fixedDeltaTime, 0, 0);
            isMoving = true;
            flip.flipX = true;
        }

        if (IC.moveRight)
        {
            transform.Translate(playerSpeed * Time.fixedDeltaTime, 0, 0);
            isMoving = true;
            flip.flipX = false;
        }

        if (IC.moveUp)
        {
            transform.Translate(0, playerSpeed * Time.fixedDeltaTime, 0);
        }

        if (IC.moveDown)
        {
            transform.Translate(0, -playerSpeed * Time.fixedDeltaTime, 0);
        }
    }

    private void FlipCharacter()
    {
        /***********************Flipping The Player***********************/

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
    }

    private void Animator()
    {
        /***********************Controlling the Animator***********************/
        if (!IC.moveLeft && !IC.moveRight && !IC.moveUp && !IC.moveDown)
        {
            animator.SetFloat("Speed", 0);
        }

        else
        {
            animator.SetFloat("Speed", 1);
        }
    }
}
