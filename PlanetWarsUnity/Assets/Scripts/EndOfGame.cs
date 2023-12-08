using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
    private List<Planet> planetList = new();
    [SerializeField] GameObject gameObjectVictoryPanel;
    [SerializeField] GameObject gameObjectGameOverPanel;
    [SerializeField] GameObject backToMenu;
    private GameManager gameManager;
    private bool gameFinished;

    private void Start()
    {
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        planetList.AddRange(planetsArray);
        gameManager = GameManager.GetInstance();
    }

    private void OnEnable()
    {
        gameFinished = false;
    }

    private void Update()
    {
        if (CheckEndOfGame() && !gameFinished)
        {
            gameFinished = true;
            if (planetList[0].Owner == "player")
            {
                Victory();
            }
            else
            {
                Defeat();
            } 
        }
    }

    private bool CheckEndOfGame()
    {
        int playerPlanets = planetList.Count(planet => planet.Owner == "player");
        int AIPlanets = planetList.Count(planet => planet.Owner == "AI");

        return playerPlanets == 0 || AIPlanets == 0;
    }

    private void Victory()
    {
        Time.timeScale = 0;
        gameObjectVictoryPanel.SetActive(true);
        backToMenu.SetActive(true);
        gameManager.AddCrowns();
    }

    private void Defeat()
    {
        Time.timeScale = 0;
        gameObjectGameOverPanel.SetActive(true);
        backToMenu.SetActive(true);
    }
}
