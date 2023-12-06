using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class AI : MonoBehaviour
{
    [SerializeField] int criticalNum = 7;
    public float waitingTime = 2.5f; // Waiting time between AI's actions
    public int sendTroops = 5; // Quantity of troops transferred with a click.
    private Planet planetOrig; // Planet sending the troops
    private Planet targetPlanet;
    private Planet helpPlanet;
    private List<Planet> planetList = new List<Planet>();

    private void OnEnable() 
    {
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        planetList.AddRange(planetsArray.ToArray());
        StartCoroutine(AIManagement());
    }

    // Simulates the AI's decision
    private void MakeDecision()
    {
        int totalPlanets = planetList.Count;
        // We reset the decisions
        planetOrig = null;
        targetPlanet = null;
        helpPlanet = null;

        // We reset the values of the minimums and maximumns
        int minPlayer = 1000000;
        int maxIA = 0;
        int minIA = 1000000;
        int numberPlayer = 0;
        for (int i = 0; i < totalPlanets; i++)
        {
            if (planetList[i].owner == "player")
            {
                numberPlayer ++;
                if (planetList[i].Troops < minPlayer)
                {
                    minPlayer = planetList[i].Troops;
                    targetPlanet = planetList[i];
                }
            }
            else if (planetList[i].owner == "AI")
            {
                if (planetList[i].Troops < minIA)
                {
                    minIA = planetList[i].Troops;
                    helpPlanet = planetList[i];
                }
                if (planetList[i].Troops >= maxIA)
                {
                    maxIA = planetList[i].Troops;
                    planetOrig = planetList[i];
                }
            }
        }
        if (planetOrig.Troops > 5)
        {
            if (helpPlanet.Troops < criticalNum && helpPlanet != planetOrig && numberPlayer > 1)
            {
                planetOrig.SendSpaceship(helpPlanet.transform.position);
            }
            else
            {
                planetOrig.SendSpaceship(targetPlanet.transform.position);
            }
        }
    }

    private IEnumerator AIManagement()
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
