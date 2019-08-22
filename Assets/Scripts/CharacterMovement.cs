using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    InputController IC;
    //WeaponController WC;
    CharacterController CC;
    public Animator animator;

    private bool isMoving = false;
    public SpriteRenderer flip;

    private void Start()
    {
        IC = InputController.instance;
        //WC = WeaponController.instance;
        CC = CharacterController.instance;
    }

    private void FixedUpdate()
    {
        /***********************Moving the Character***********************/
        if (IC.moveLeft)
        {
            transform.Translate(-CC.playerSpeed * Time.fixedDeltaTime, 0, 0);
            isMoving = true;
            flip.flipX = true;
        }

        if (IC.moveRight)
        {
            transform.Translate(CC.playerSpeed * Time.fixedDeltaTime, 0, 0);
            isMoving = true;
            flip.flipX = false;
        }

        if (IC.moveUp)
        {
            transform.Translate(0, CC.playerSpeed * Time.fixedDeltaTime, 0);
        }

        if (IC.moveDown)
        {
            transform.Translate(0, -CC.playerSpeed * Time.fixedDeltaTime, 0);
        }

        
        /***********************Flipping The Player***********************/

        if (IC.mouseXCoord >= 0 && !isMoving)
        {
            flip.flipX = false;
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (IC.mouseXCoord < 0 && !isMoving)
        {
            flip.flipX = true;
            //transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        else
        {
            isMoving = false;
        }

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
