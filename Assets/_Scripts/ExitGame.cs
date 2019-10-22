using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void ExitApplication()
    {
        Debug.Log("Exited Application");
        Application.Quit();
    }
}
