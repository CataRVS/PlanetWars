using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject startMenu;

    public void Start()
    {

        startMenu.SetActive(true);
        game.SetActive(false);
        Time.timeScale = 1;

    }
}
