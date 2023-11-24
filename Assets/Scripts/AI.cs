using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class AI : MonoBehaviour
{
    [SerializeField] int criticalNum = 7;
    public float waitingTime = 2.0f; // Waiting time between AI's actions
    public int sendTroops = 5; // Quantity of troops transferred with a click.
    private Planet planetOrig; // Planet sending the troops
    private Planet targetPlanet;
    private Planet helpPlanet;
    private List<Planet> planetList = new List<Planet>();

    void OnEnable()
    {
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        planetList.AddRange(planetsArray.OrderBy(planet => planet.transform.position.x).ToArray());
        StartCoroutine(AIManagement());
    }

    // Simulates the AI's decision
    void MakeDecision()
    {
        planetOrig = null;
        targetPlanet = null;
        helpPlanet = null;
        int minPlayer = 1000000;
        int maxIA = 0;
        int minIA = 1000000;
        int totalPlanets = planetList.Count;
        for (int i = 0; i < totalPlanets; i++)
        {
            if (planetList[i].owner == "player")
            {
                if (planetList[i].troops < minPlayer)
                {
                    minPlayer = planetList[i].troops;
                    targetPlanet = planetList[i];
                }
            }
            else if (planetList[i].owner == "AI")
            {
                if (planetList[i].troops < minIA)
                {
                    minIA = planetList[i].troops;
                    helpPlanet = planetList[i];
                }
                else if (planetList[i].troops > maxIA)
                {
                    maxIA = planetList[i].troops;
                    planetOrig = planetList[i];
                }
            }
        }
        if (planetOrig.troops > 5)
        {
            if (helpPlanet.troops < criticalNum && helpPlanet != planetOrig)
            {
                planetOrig.SendSpaceship(helpPlanet.transform.position);

            }
            else
            {
                planetOrig.SendSpaceship(targetPlanet.transform.position);
            }

        }
    }

    IEnumerator AIManagement()
    {
        float timer = 0f;
        while (true)
        {
            // Adds the time difference
            timer += Time.deltaTime;

            // If the waiting time has passed we restart the timer and make a decision
            if (timer >= waitingTime)
            {
                MakeDecision();
                timer = 0f;
            }
            yield return null;
        }
    }

}
