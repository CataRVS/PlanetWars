using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject gameObjectPauseMenu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject startMenu;

    public void Start()
    {
        startMenu.SetActive(true);
        gameObjectPauseMenu.SetActive(false);
        game.SetActive(false);
        Time.timeScale = 1;
    }

    public void Play()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
        game.SetActive(true);
        gameObjectPauseMenu.SetActive(false);
        Debug.Log("Play");
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
        game.SetActive(false);
        startMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Debug.Log("Exiting game...");
        //Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
