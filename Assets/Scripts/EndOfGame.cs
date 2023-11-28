using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
    private List<Planet> planetList = new List<Planet>();
    [SerializeField] GameObject victory;
    [SerializeField] GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        Planet[] planetsArray = FindObjectsOfType<Planet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckEndOfGame())
        {
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
    bool CheckEndOfGame()
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

    void Victory()
    {
        Debug.Log("Victory!");
        victory.SetActive(true);
    }

    void Defeat()
    {
        Debug.Log("Game Over!");
        gameOver.SetActive(true);
    }
}
