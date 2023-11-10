using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Planet PlanetOrig = null; // El planeta desde el que se envían las tropas.
    private Planet PlanetDest = null; // El planeta al que se van a transferir las tropas.

    public int sendTroops = 5; // Cantidad predeterminada de tropas a transferir con un click.

    void Update()
    {
        // Detecta el clic del jugador en un planeta.
        if (Input.GetMouseButtonDown(0))
        {
            DetectClickedPlanet();
        }

        // Realiza la conquista del planeta si se han seleccionado el origen y el destino.
        if (PlanetOrig != null && PlanetDest != null)
        {
            if (PlanetOrig.owner == "player" && PlanetDest.owner == "AI")
            {
                ConquerPlanet(PlanetOrig, PlanetDest);
                CleanSection();
            }
            else if (PlanetOrig.owner == "player" && PlanetDest.owner == "player")
            { 
                ReforceDefensePlanet(PlanetOrig, PlanetDest);
                CleanSection();
            }
            else
            {
                CleanSection();
            }
        }
    }

    void DetectClickedPlanet()
    {
        // Raycast desde la posición del clic del mouse.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Obtén el componente Planeta del objeto clickeado.
            Planet clickedPlanet = hit.collider.GetComponent<Planet>();

            if (clickedPlanet != null)
            {
                // Si es el primer clic, selecciona el planeta como origen.
                if (PlanetOrig == null)
                {
                    PlanetOrig = clickedPlanet;
                }
                // Si es el segundo clic, selecciona el planeta como destino.
                else if (PlanetDest == null)
                {
                    PlanetDest = clickedPlanet;
                }
            }
            else
            {
                // Si el jugador hace clic en un área vacía, limpia la selección.
                CleanSection();
            }

        }
    }

    public void ConquerPlanet(Planet PlanetOrig, Planet PlanetDest)
    {
        int troopsRemainingOrigin = 0;
        int troopsRemainingDestination = 0;

        if (PlanetOrig.troops >= sendTroops)
        {
            // Calcula las tropas restantes después de la conquista.
            troopsRemainingOrigin = PlanetOrig.troops - sendTroops;

            if (PlanetDest.troops < sendTroops)  // si el planeta destino tiene menos tropas que las que se envían, el planeta se conquista.
            {
                PlanetDest.owner = "player";  // conquistamos
                troopsRemainingDestination = sendTroops - PlanetDest.troops;
    
            }
            else  // el planeta destino tiene más tropas de las que vamos a enviar, no conquistamos
            {
                troopsRemainingDestination = PlanetDest.troops - sendTroops;
            }

            // Actualiza el color del TextMesh del planeta destino según las condiciones.
            PlanetDest.UpdateColorTextMesh();

            // Actualiza la cantidad de tropas en el planeta origen y destino.
            PlanetOrig.troops = troopsRemainingOrigin;
            PlanetDest.troops = troopsRemainingDestination;

            // Actualiza el TextMesh de los planetas para mostrar las cantidades actualizadas.
            PlanetOrig.UpdateTextMesh();
            PlanetDest.UpdateTextMesh();
        }
    }

    public void ReforceDefensePlanet(Planet PlanetOrig, Planet PlanetDest)
    {
        int troopsRemainingOrigin = 0;
        int troopsRemainingDestination = 0;

        if (PlanetOrig.troops >= sendTroops && PlanetDest.troops < PlanetDest.capacity)
        {
            // Calcula las tropas restantes después de la conquista.
            

            if (PlanetDest.troops + sendTroops <= PlanetDest.capacity)  // el planeta destino tiene más tropas de las que vamos a enviar, no conquistamos
            {
                troopsRemainingDestination = PlanetDest.troops + sendTroops;
                troopsRemainingOrigin = PlanetOrig.troops - sendTroops;
            }
            else
            {
                troopsRemainingDestination = PlanetDest.capacity;
                troopsRemainingOrigin = PlanetOrig.troops + (PlanetDest.troops - PlanetDest.capacity);
            }

            // Actualiza el color del TextMesh del planeta destino según las condiciones.
            PlanetDest.UpdateColorTextMesh();

            // Actualiza la cantidad de tropas en el planeta origen y destino.
            PlanetOrig.troops = troopsRemainingOrigin;
            PlanetDest.troops = troopsRemainingDestination;

            // Actualiza el TextMesh de los planetas para mostrar las cantidades actualizadas.
            PlanetOrig.UpdateTextMesh();
            PlanetDest.UpdateTextMesh();
        }
    }

    void CleanSection()
    {
        // Limpia la selección de origen y destino para el siguiente movimiento.
        PlanetOrig = null;
        PlanetDest = null;
    }
    
    void UpdateColorTextMesh(Planet planet, bool showBlue)
    {
        TextMesh textMesh = planet.GetComponentInChildren<TextMesh>();
        if (textMesh != null)
        {
            if (showBlue)
            {
                textMesh.color = Color.blue;
            }
            else
            {
                textMesh.color = Color.red;
            }
        }
    }
}

