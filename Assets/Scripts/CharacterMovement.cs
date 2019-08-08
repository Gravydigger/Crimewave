using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    /*
    bool moveLeft;
    bool moveRight;
    bool moveUp;
    bool moveDown;
    */
    InputController IC = InputController.instance;

    public float playerSpeed = 6;

    private void Start()
    {
        IC = InputController.instance;
    }

    void Update()
    {
        //Keyboard Controls

        /*moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);
        moveUp = Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.S);
        */
    }

    private void FixedUpdate()
    {
        /***********************Moving the Character***********************/
        if (IC.moveLeft)
        {
            transform.Translate(-playerSpeed * Time.fixedDeltaTime, 0, 0);
        }

        if (IC.moveRight)
        {
            transform.Translate(playerSpeed * Time.fixedDeltaTime, 0, 0);
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
}
