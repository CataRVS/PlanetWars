using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
    private List<Planet> planetList = new();
    [SerializeField] GameObject gameObjectVictoryPanel;
    [SerializeField] GameObject gameObjectGameOverPanel;
    [SerializeField] GameObject backToMenu;
    private GameManager gameManager;
    private bool gameFinished;
    // Start is called before the first frame update
    private void Start()
    {
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        planetList.AddRange(planetsArray);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        gameFinished = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (CheckEndOfGame() && !gameFinished)
        {
            gameFinished = true;
            if (planetList[0].owner == "player")
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
        int totalPlanets = planetList.Count;
        int playerPlanets = 0;
        int AIPlanets = 0;
        for (int i = 0; i < totalPlanets; i++)
        {
            if (planetList[i].owner == "player")
            {
                playerPlanets++;
            }
            else if (planetList[i].owner == "AI")
            {
                AIPlanets++;
            }
        }
        if (playerPlanets == 0 || AIPlanets == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
