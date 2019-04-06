using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Variables
    public static bool GameIsPaused = false;

    public GameObject buttonsUI;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    #endregion
    #region Pause Menu
    public void Awake()
    {
        buttonsUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
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

        //detect if all the gates are destroyed and bring up the game over menu
        if (GameObject.FindObjectOfType<GateHealth>() == null)
        {
            GameOver();
        }
    }
    #endregion
    #region Buttons
    //Pause game and bring up the game over menu
    void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        buttonsUI.SetActive(false);
        /*Time.timeScale = 0f;
        GameIsPaused = true;*/

    }

    public void Restart()
    {

        gameOverMenuUI.SetActive(false);
        buttonsUI.SetActive(true);
        /*Time.timeScale = 1f;
        GameIsPaused = false;*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Resume()
    {
        //disable the pause menu
        pauseMenuUI.SetActive(false);
        buttonsUI.SetActive(true);
        //Resume time
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //Activate the pause menu
        pauseMenuUI.SetActive(true);
        buttonsUI.SetActive(false);
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
