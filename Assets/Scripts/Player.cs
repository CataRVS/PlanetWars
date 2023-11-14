using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Planet PlanetOrig = null; // Planet from which the troops are sent
    private Planet PlanetDest = null; // Planet to which the troops are sent

    [SerializeField] int sendTroops = 5; // Amout of troops sent by default

    void Update()
    {
        // We detect the mouse click
        if (Input.GetMouseButtonDown(0))
        {
            DetectClickedPlanet();
        }

        // If have already selected both planets, we conquer or defend depending on the destination planet.
        if (PlanetOrig != null && PlanetDest != null)
        {
            if (PlanetDest.owner == "AI")
            {
                ConquerPlanet(PlanetOrig, PlanetDest);
            }
            else if (PlanetDest.owner == "player")
            {
                if (PlanetOrig != PlanetDest)
                {
                    ReinforceDefensePlanet(PlanetOrig, PlanetDest);
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
                if (PlanetOrig == null && clickedPlanet.owner != "AI")
                {
                    PlanetOrig = clickedPlanet;
                    Debug.Log("Planeta origen seleccionado");
                }
                // If it's the second click, it's the destination
                else if (PlanetDest == null && PlanetOrig != null)
                {
                    PlanetDest = clickedPlanet;
                    Debug.Log("Planeta destino seleccionado");
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

    public void ConquerPlanet(Planet PlanetOrig, Planet PlanetDest)
    {
        Debug.Log("Conquistando planeta");

        // If the origin planet hasn't got enough troops, we don't try to conquer
        if (PlanetOrig.troops >= sendTroops)
        {
            // We calculate the number of troops left in each planet
            int troopsRemainingOrigin = PlanetOrig.troops - sendTroops;
            int troopsRemainingDestination = 0;

            // If the troops sent are greater than the ones in the planet, we conquer the planet
            if (PlanetDest.troops < sendTroops)
            {
                PlanetDest.owner = "player";
                troopsRemainingDestination = sendTroops - PlanetDest.troops;
    
            }
            // If there aren't enough troops, we don't conquer
            else
            {
                troopsRemainingDestination = PlanetDest.troops - sendTroops;
            }

            // Update the color of the text
            PlanetDest.UpdateColorTextMesh();

            // Update the number of troops in origin and in the destination
            PlanetOrig.troops = troopsRemainingOrigin;
            PlanetDest.troops = troopsRemainingDestination;

            // Update the text of the planets indicating the number of troops
            PlanetOrig.UpdateTextMesh();
            PlanetDest.UpdateTextMesh();
        }
    }

    public void ReinforceDefensePlanet(Planet PlanetOrig, Planet PlanetDest)
    {
        // If the origin planet hasn't got enough troops or the destination is already full, we don't defend
        if (PlanetOrig.troops >= sendTroops && PlanetDest.troops < PlanetDest.capacity)
        {
            // We calculate the number of troops left in each planet
            int troopsRemainingOrigin = 0;
            int troopsRemainingDestination = 0;

            // If we don't reach the capacity with the actual number of troops sent, we send them all
            if (PlanetDest.troops + sendTroops <= PlanetDest.capacity)
            {
                troopsRemainingDestination = PlanetDest.troops + sendTroops;
                troopsRemainingOrigin = PlanetOrig.troops - sendTroops;
            }
            // If we reach the capacity with the actual number of troops sent, we only send the ones needed to reach the capacity
            else
            {
                troopsRemainingDestination = PlanetDest.capacity;
                troopsRemainingOrigin = PlanetOrig.troops + (PlanetDest.troops - PlanetDest.capacity);
            }

            // Update the number of troops in origin and in the destination
            PlanetOrig.troops = troopsRemainingOrigin;
            PlanetDest.troops = troopsRemainingDestination;

            // Update the text of the planets indicating the number of troops
            PlanetOrig.UpdateTextMesh();
            PlanetDest.UpdateTextMesh();
        }
    }

    void CleanSection()
    {
        // We clean the selection of planets selected
        PlanetOrig = null;
        PlanetDest = null;
    }
}

