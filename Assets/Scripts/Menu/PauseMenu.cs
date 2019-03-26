using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    #region Variables
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    #endregion
    #region Pause Menu
    // Update is called once per frame
    void Update () {
        //if pause menu is accessed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}
    #endregion
    #region Buttons
    public void Resume()
    {
        //disable the pause menu
        pauseMenuUI.SetActive(false);
        //Resume time
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //Activate the pause menu
        pauseMenuUI.SetActive(true);
        //freeze time
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        //Load menu scene and resume time
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame() 
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
    #endregion
}
