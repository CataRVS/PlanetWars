using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class GameInitializer : MonoBehaviour
{
    private void OnEnable()
    {
        PlanetInitilizer();
        SpaceshipInitializer();
    }

    private void PlanetInitilizer()
    {
        List<Planet> planetList = new();
        // We look for the planets on the scene and we order by position them to assign an owner to each one
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        int totalPlanets = planetsArray.Length;
        planetList.AddRange(planetsArray.OrderBy(planet => planet.transform.position.x).ToArray());
        int half = totalPlanets / 2;
        for (int i = 0; i < totalPlanets; i++)
        {
            if (i < half)
            {
                // The planets on the left will be assigned to the player
                planetList[i].Owner = "player";
            }
            else
            {
                // The planets on the left will be assigned to the AI
                planetList[i].Owner = "AI";
            }
            planetList[i].UpdateColorTextMesh();
        }
    }

    private void SpaceshipInitializer()
    {
        Spaceship[] spaceshipsArray = FindObjectsOfType<Spaceship>();
        foreach (Spaceship spaceship in spaceshipsArray)
        {
            Destroy(spaceship.gameObject);
        }
    }
}
