using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetInitializer : MonoBehaviour
{
    private List<Planet> planetList = new List<Planet>();  // Una lista de planetas en tu escena.

    void Awake()
    {
        // We look for the planets on the scene and we order by position them to assign an owner to each one
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        planetList.AddRange(planetsArray.OrderBy(planet => planet.transform.position.x).ToArray());

        int totalPlanets = planetList.Count;
        int half = totalPlanets / 2;

        for (int i = 0; i < totalPlanets; i++)
        {
            if (i < half)
            {
                // The planets on the left will be assigned to the player
                planetList[i].owner = "player";
            }
            else
            {
                // The planets on the left will be assigned to the AI
                planetList[i].owner = "AI";
            }
            planetList[i].UpdateColorTextMesh();
        }
    }
}

