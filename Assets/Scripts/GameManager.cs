using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    CharacterManager CM;

    public Texture2D crossHair;
    private Vector2 crossHairOffset;
    public GameObject gameOverLost;
    public GameObject gameOverWon;
    public GameObject gamePaused;
    public bool gameOver = false;
    public bool isGamePaused = false;

    private void Awake()
    {
        gameOverWon.SetActive(false);
        gameOverLost.SetActive(false);
        gamePaused.SetActive(false);
        instance = this;
        crossHairOffset = new Vector2(3.5f, 3.5f);

        EnemyManager.onAnyEnemyDeath.AddListener(OnEnemyDead);
        CharacterManager.onPlayerDeath.AddListener(OnPlayerDeath);
    }

    private void Start()
    {
        //sets the cursor to a cross hair
        Cursor.SetCursor(crossHair, crossHairOffset, CursorMode.Auto);
        CM = CharacterManager.instance;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    void OnPlayerDeath()
    {
        gameOverLost.SetActive(true);
        gameOver = true;
    }

    //Get called when an enemies dies
    void OnEnemyDead()
    {
        //Finds all enemies with the script "EnemyManager".
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();

        //If any enemy is stil alive, continue the game.
        bool allDead = true;
        foreach (EnemyManager enemy in enemies)
        {
            if (!enemy.isDead)
            {
                allDead = false;
                return;
            }
        }

        //If they are all dead, show the win screen.
        if (allDead)
        {
            gameOver = true;
            gameOverWon.SetActive(true);
            Debug.Log("GameOver");
        }
    }

    public void PauseMenu()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            gamePaused.SetActive(true);
            return;
        }

        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1;
            gamePaused.SetActive(false);
            return;
        }

    }
}
