using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

    public void NewGame()
    {
        Application.LoadLevel(1);
    }

    public void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ReturnToMenu()
    {
        Application.LoadLevel(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
