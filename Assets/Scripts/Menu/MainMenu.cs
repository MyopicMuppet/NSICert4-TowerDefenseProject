using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    #region Buttons
    public void PlayGame()
    {
        //Load the next scene when the play button is pressed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void ExitGame()
    {
        //create a debug log
        Debug.Log("Exit");
        //makes the exit button exit the game
        Application.Quit();
    }
    #endregion

}
