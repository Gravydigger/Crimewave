using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    InputController IC;
    //WeaponController WC;
    CharacterManager CM;
    public Animator animator;

    private bool isMoving = false;
    public SpriteRenderer flip;

    private void Start()
    {
        IC = InputController.instance;
        //WC = WeaponController.instance;
        CM = CharacterManager.instance;
    }

    private void FixedUpdate()
    {
        //Moves the character in cardinal directions
        MoveCharacter();
        //Flips the player sprite depending if the player is stationary
        FlipCharacter();
        //controls the animator to spawn between standing and running
        Animator();
    }

    private void MoveCharacter()
    {
        /***********************Moving the Character***********************/
        if (IC.moveLeft)
        {
            transform.Translate(-CM.playerSpeed * Time.fixedDeltaTime, 0, 0);
            isMoving = true;
            flip.flipX = true;
        }

        if (IC.moveRight)
        {
            transform.Translate(CM.playerSpeed * Time.fixedDeltaTime, 0, 0);
            isMoving = true;
            flip.flipX = false;
        }

        if (IC.moveUp)
        {
            transform.Translate(0, CM.playerSpeed * Time.fixedDeltaTime, 0);
        }

        if (IC.moveDown)
        {
            transform.Translate(0, -CM.playerSpeed * Time.fixedDeltaTime, 0);
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
