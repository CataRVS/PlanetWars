using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int sendTroops = 5; // Amout of troops sent by default
    private float scaleFactor = 1.1f;
    private Planet planetDest; // Planet to which the troops are sent
    private bool selectingPlanetsOrig; // Is true if the mouse is down while selecting planets
    private List<Planet> planetsOrig = new();

    void Update()
    {
        // You are pressing the mouse button
        if (Input.GetMouseButton(0))
        {
            // You are selecting the first origin planet
            if (planetsOrig.Count == 0)
            {
                selectingPlanetsOrig = true;
                SelectPlanetsOrig();
            }
            // You are selecting more than one origin planet
            else if (selectingPlanetsOrig)
            {
                SelectPlanetsOrig();
            }
            // You are selecting your destination planet
            else
            {
                SelectPlanetDest();
            }
        }
        // You are not pressing the mouse button
        else
        {
            selectingPlanetsOrig = false;
        }
        // If yoou have already selected both type of planets we send the troops to the destination.
        if (planetsOrig.Count != 0 && !selectingPlanetsOrig && planetDest != null)
        {
            SendSpaceshipsToDest();
            CleanSection();
        }
    }
    void SelectPlanetsOrig()
    {
        // Raycast to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // We get the planet clicked if it exists
            Planet clickedPlanet = hit.collider.GetComponent<Planet>();
            if (clickedPlanet != null)
            {
                // If the player is the owner of the planet and you havent selected it already we add it to the list
                if (clickedPlanet.owner == "player" && !planetsOrig.Contains(clickedPlanet))
                {
                    planetsOrig.Add(clickedPlanet);
                    clickedPlanet.transform.localScale = clickedPlanet.transform.localScale * scaleFactor;
                }
            }
        }
        
    }

    public void SelectPlanetDest()
    {
        // Raycast to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // We get the planet clicked if it exists
            Planet clickedPlanet = hit.collider.GetComponent<Planet>();

            if (clickedPlanet != null)
            {
                planetDest = clickedPlanet;
            }
            // If the player doesn't hit the planet, we clean the selection
            else
            {
                CleanSection();
            }
        }
        // If the player doesn't hit the planet, we clean the selection
        else
        {
            CleanSection();
        }
    }

    void SendSpaceshipsToDest()
    {
        foreach (Planet planet in planetsOrig)
        {
            if (planet != planetDest && planet.Troops >= sendTroops && planet.owner == "player")
            {
                planet.SendSpaceship(planetDest.transform.position);
            }
        }
    }

    void CleanSection()
    {
        foreach (Planet planet in planetsOrig)
        {
            planet.transform.localScale = planet.transform.localScale / scaleFactor;
        }
        // We clean the selection of planets selected
        planetsOrig.Clear();
        planetDest = null;
    }
}
