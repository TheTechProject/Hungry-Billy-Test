using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    /// <summary>
    /// Starts the game from the first scene.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
