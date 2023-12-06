using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int sendTroops = 5; // Amout of troops sent by default
    private Planet planetOrig; // Planet from which the troops are sent
    private Planet planetDest; // Planet to which the troops are sent
    private List<Planet> selectedPlanets = new List<Planet>();

    void Update()
    {
        // We detect the mouse click
        if (Input.GetMouseButtonDown(0))
        {
            DetectClickedPlanet();
        }

        // If the mouse button is held down, and we have selected planets, continue the selection.
        if (Input.GetMouseButton(0) && selectedPlanets.Count > 0)
        {
            ContinueSelection();
        }

        // If have already selected both planets, we conquer or defend depending on the destination planet.
        if (planetOrig != null && planetDest != null)
        {
            if (planetDest.owner == "AI")
            {
                //ConquerPlanet(planetOrig, planetDest);
                if (planetOrig.troops >= sendTroops)
                {
                    planetOrig.SendSpaceship(planetDest.transform.position);
                }
            }
            else if (planetDest.owner == "player")
            {
                if (planetOrig != planetDest)
                {
                    if (planetOrig.troops >= sendTroops)
                    {
                        planetOrig.SendSpaceship(planetDest.transform.position);
                    }
                    //ReinforceDefensePlanet(planetOrig, planetDest);
                }
            }
            CleanSection();
        }
    }
    void ContinueSelection()
{
    // Additional logic for continuous selection while the mouse button is held down.
    // You can update the selection appearance or provide feedback.
    // For example, you can change the color of the selected planets.
    foreach (Planet planet in selectedPlanets)
    {
        // Update the appearance of the selected planets (change color, outline, etc.).
        // Example: planet.SetSelectedColor();
    }
}

    public void DetectClickedPlanet()
    {
        // Raycast to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // We get the planet clicked if it exists
            Planet clickedPlanet = hit.collider.GetComponent<Planet>();

            if (clickedPlanet != null)
            {
                // If it's the first click, it's the origin so it must be a player's planet.
                if (planetOrig == null && clickedPlanet.owner != "AI")
                {
                    planetOrig = clickedPlanet;
                }
                // If it's the second click, it's the destination
                else if (planetDest == null && planetOrig != null)
                {
                    planetDest = clickedPlanet;
                }
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

    void CleanSection()
    {
        // We clean the selection of planets selected
        planetOrig = null;
        planetDest = null;
    }
}
