using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject gameObjectPauseMenu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject gameObjectPanelInstructions;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject backToMenu;

    public void Play()
    {
        startMenu.SetActive(false);
        game.SetActive(true);
        gameObjectPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameObjectPauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObjectPauseMenu.SetActive(false);
    }

    public void Restart()
    {
        game.SetActive(false);
        game.SetActive(true);
        gameObjectPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        victoryPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        backToMenu.SetActive(false);
        game.SetActive(false);
        startMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Debug.Log("Exiting game...");

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }

    public void InstrucionsPanel()
    {
        if (gameObjectPanelInstructions.activeSelf)
        {
            Time.timeScale = 1;
            gameObjectPanelInstructions.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            gameObjectPanelInstructions.SetActive(true);
        }
    }
}
