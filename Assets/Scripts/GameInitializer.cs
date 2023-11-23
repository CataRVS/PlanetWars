using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    private List<Planet> planetList = new List<Planet>();  // Una lista de planetas en tu escena.

    private void OnEnable()
    {
        
        pauseButton.SetActive(true);
        Initilizer();
    }

    void Initilizer()
    {
        planetList.Clear();
        // We look for the planets on the scene and we order by position them to assign an owner to each one
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        Debug.Log(planetsArray.Length);
        int totalPlanets = planetsArray.Length;
        for (int i = 0; i < totalPlanets; i++)
        {
            planetsArray[i].gameObject.SetActive(true);
            Debug.Log(i);
        }
        planetList.AddRange(planetsArray.OrderBy(planet => planet.transform.position.x).ToArray());
        Debug.Log(planetList.Count);
        int half = totalPlanets / 2;
            for (int i = 0; i < totalPlanets; i++)
        {
            if (!planetList[i].gameObject.activeSelf)
            {
                planetList[i].gameObject.SetActive(true);
            }
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

