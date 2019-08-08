using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    bool moveLeft;
    bool moveRight;
    bool moveUp;
    bool moveDown;

    public float playerSpeed = 6;

    void Update()
    {
        //Keyboard Controls
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);
        moveUp = Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.S);
    }

    private void FixedUpdate()
    {
        /***********************Moving the Character***********************/
        if (moveLeft)
        {
            transform.Translate(-playerSpeed * Time.fixedDeltaTime, 0, 0);
        }

        if (moveRight)
        {
            transform.Translate(playerSpeed * Time.fixedDeltaTime, 0, 0);
        }

        if (moveUp)
        {
            transform.Translate(0, playerSpeed * Time.fixedDeltaTime, 0);
        }

        if (moveDown)
        {
            transform.Translate(0, -playerSpeed * Time.fixedDeltaTime, 0);
        }
    }
}
