using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Planet planetOrig = null; // Planet from which the troops are sent
    private Planet planetDest = null; // Planet to which the troops are sent

    [SerializeField] int sendTroops = 5; // Amout of troops sent by default

    void Update()
    {
        // We detect the mouse click
        if (Input.GetMouseButtonDown(0))
        {
            DetectClickedPlanet();
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

    public void ConquerPlanet(Planet planetOrig, Planet planetDest)
    {
        Debug.Log("Conquistando planeta");

        // If the origin planet hasn't got enough troops, we don't try to conquer
        if (planetOrig.troops >= sendTroops)
        {
            // We calculate the number of troops left in each planet
            int troopsRemainingOrigin = planetOrig.troops - sendTroops;
            int troopsRemainingDestination;

            // If the troops sent are greater than the ones in the planet, we conquer the planet
            if (planetDest.troops < sendTroops)
            {
                planetDest.owner = "player";
                troopsRemainingDestination = sendTroops - planetDest.troops;

            }
            // If there aren't enough troops, we don't conquer
            else
            {
                troopsRemainingDestination = planetDest.troops - sendTroops;
            }

            // Update the color of the text
            planetDest.UpdateColorTextMesh();

            // Update the number of troops in origin and in the destination
            planetOrig.troops = troopsRemainingOrigin;
            planetDest.troops = troopsRemainingDestination;

            // Update the text of the planets indicating the number of troops
            planetOrig.UpdateTextMesh();
            planetDest.UpdateTextMesh();
        }
    }

    public void ReinforceDefensePlanet(Planet planetOrig, Planet planetDest)
    {
        // If the origin planet hasn't got enough troops or the destination is already full, we don't defend
        if (planetOrig.troops >= sendTroops && planetDest.troops < planetDest.capacity)
        {
            // We calculate the number of troops left in each planet
            int troopsRemainingOrigin;
            int troopsRemainingDestination;

            // If we don't reach the capacity with the actual number of troops sent, we send them all
            if (planetDest.troops + sendTroops <= planetDest.capacity)
            {
                troopsRemainingDestination = planetDest.troops + sendTroops;
                troopsRemainingOrigin = planetOrig.troops - sendTroops;
            }
            // If we reach the capacity with the actual number of troops sent, we only send the ones needed to reach the capacity
            else
            {
                troopsRemainingDestination = planetDest.capacity;
                troopsRemainingOrigin = planetOrig.troops + (planetDest.troops - planetDest.capacity);
            }

            // Update the number of troops in origin and in the destination
            planetOrig.troops = troopsRemainingOrigin;
            planetDest.troops = troopsRemainingDestination;

            // Update the text of the planets indicating the number of troops
            planetOrig.UpdateTextMesh();
            planetDest.UpdateTextMesh();
        }
    }

    void CleanSection()
    {
        // We clean the selection of planets selected
        planetOrig = null;
        planetDest = null;
    }
}

