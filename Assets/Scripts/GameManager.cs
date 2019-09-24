using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CharacterManager CM;

    public Texture2D crossHair;
    public Vector2 crossHairOffset;
    public GameObject gameOver;

    private void Awake()
    {
        gameOver.SetActive(false);
        CM = CharacterManager.instance;
    }

    private void Start()
    {
        //sets what the cursor looks like
        Cursor.SetCursor(crossHair, crossHairOffset, CursorMode.Auto);
    }

    void Update()
    {
        //Checks if the player is dead
        if (CM.isPlayerDead)
        {
            gameOver.SetActive(true);
        }
    }
}
