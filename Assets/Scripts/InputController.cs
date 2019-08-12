using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //Allows other Scrips to call the Input Controller
    //place this line into function Start(): IC = InputController.instance;
    public static InputController instance;

    
    //Allows for the keybinds to be changed in-game (WIP)
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public bool moveLeft, moveRight, moveUp, moveDown;

    public float mouseXCoord, mouseYCoord;

    void Start()
    {
        instance = this;

        //KeyCodeDatabase[Inputs.Left] = KeyCode.A;
    }

    //public Dictionary<Inputs, KeyCode> KeyCodeDatabase = new Dictionary<Inputs, KeyCode>();

    /*public enum Inputs
    {
        Left, Right, Up, Down
    }*/



    void Update()
    {
        /********************Player Controls******************/
        /*~~~~~~~~~~~~~~~~~Player Movement~~~~~~~~~~~~~~~~~*/
        moveLeft = Input.GetKey(leftKey);
        moveRight = Input.GetKey(rightKey);
        moveUp = Input.GetKey(upKey);
        moveDown = Input.GetKey(downKey);

        /*~~~~~~~~~~~~~~~~~Mouse Movement~~~~~~~~~~~~~~~~~*/
        mouseXCoord = Input.mousePosition.x - (Screen.width / 2);
        mouseYCoord = Input.mousePosition.y - (Screen.height / 2);

        //use this to allow the player to change the keybinds (WIP)
        //string s  = Input.inputString;
    }
}
