using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float scaleFactor = 1.1f;
    private Planet planetDest; // Planet to which the troops are sent
    private bool selectingPlanetsOrig = false; // Is true if the mouse is down while selecting planets
    private bool selectingPlanetDest = false;
    private List<Planet> planetsOrig = new();
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    private void Update()
    {
        // You are pressing the mouse button
        if (Input.GetMouseButton(0))
        {
            // You are selecting the first origin planet
            if (planetsOrig.Count == 0 && !selectingPlanetDest)
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
            else if (!selectingPlanetDest)
            {
                SelectPlanetDest();
                selectingPlanetDest = true;
            }
        }
        // You are not pressing the mouse button
        else
        {
            selectingPlanetsOrig = false;
            selectingPlanetDest = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            UpgradePlanet();
        }
        // If yoou have already selected both type of planets we send the troops to the destination.
        if (planetsOrig.Count != 0 && !selectingPlanetsOrig && planetDest != null)
        {
            SendSpaceshipsToDest();
            CleanSection();
        }
    }

    private void SelectPlanetsOrig()
    {
        Planet clickedPlanet = DetectClickedPlanet();
        if (clickedPlanet != null)
        {
            // If the player is the owner of the planet and you havent selected it already we add it to the list
            if (clickedPlanet.Owner == "player" && !planetsOrig.Contains(clickedPlanet))
            {
                planetsOrig.Add(clickedPlanet);
                clickedPlanet.transform.localScale = clickedPlanet.transform.localScale * scaleFactor;
            }
        }
    }

    private void SelectPlanetDest()
    {
        Planet clickedPlanet = DetectClickedPlanet();
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

    private void SendSpaceshipsToDest()
    {
        foreach (Planet planet in planetsOrig)
        {
            if (planet != planetDest && planet.Owner == "player")
            {
                planet.SendSpaceship(planetDest.transform.position);
            }
        }
    }

    private void CleanSection()
    {
        foreach (Planet planet in planetsOrig)
        {
            planet.transform.localScale = planet.transform.localScale / scaleFactor;
        }
        // We clean the selection of planets selected
        planetsOrig.Clear();
        planetDest = null;
    }

    private void UpgradePlanet()
    {
        Planet clickedPlanet = DetectClickedPlanet();

        if (clickedPlanet != null && gameManager.Crowns >= gameManager.BoosterCost)
        {
            gameManager.RemoveCrowns();
            clickedPlanet.UpgradeRegenerationTime();
        }
    }

    private Planet DetectClickedPlanet()
    {
        Planet clickedPlanet = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // We get the planet clicked if it exists
            clickedPlanet = hit.collider.GetComponent<Planet>();
        }
        return clickedPlanet;
    }
}
