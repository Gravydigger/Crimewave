using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private Scene activeScene;

    public Texture2D cursor;
    private Vector2 cursorOffset;

    private void Awake()
    {
        cursorOffset = Vector2.zero;
    }

    void Start()
    {
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.Auto);
    }
}
