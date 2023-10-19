using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public string menuScene;
    public string proScene;

    public BattleSystem BS;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()
    {
        if(GameIsPaused)
            {
                Resume();                
            }
            else
            {
                Pause();            
            }
    }

    public void Resume()
    {       
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GoHome()
    {
        StaticData.team.Clear();
        SceneManager.LoadScene(menuScene);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Flee()
    {
        BS.state = BattleState.LOST;
        StaticData.isBattleWon = false;
        BS._OnBattleEnd?.Invoke(); // Invoke Event                             // SCOTT EDIT - So that functions can be called for integrating to two scene
        SceneManager.LoadScene(proScene);
    }

    public void Exit()
    {
        Application.Quit();
    }    
}
