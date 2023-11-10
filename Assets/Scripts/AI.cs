using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float waitingTime = 2.0f; // Tiempo de espera entre los movimientos de la IA.
    private Planet PlanetOrig = null; // El planeta desde el que se envían las tropas.
    private Planet PlanetDest = null; // El planeta al que se van a transferir las tropas.
    public int sendTroops = 5; // Cantidad predeterminada de tropas a transferir con un click.

   /// void Start()
    

    void Update()
    {
        if (PlanetOrig != null && PlanetDest != null)
        {
            if (PlanetOrig.owner == "AI" && PlanetDest.owner == "player")
            {
                ConquerPlanet(PlanetOrig, PlanetDest);
                CleanSection();
            }
            else if (PlanetOrig.owner == "AI" && PlanetDest.owner == "AI")
            { 
                ReforceDefensePlanet(PlanetOrig, PlanetDest);
                CleanSection();
            }
            else
            {
                CleanSection();
            }
        }
        // En el turno de la IA, implementa la lógica de decisión.
        MakeDecision();

        // Simula un retraso antes de cambiar al turno del jugador.
        StartCoroutine(WaitToChangeTurn());
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
                PlanetDest.owner = "AI";  // conquistamos
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

    public void ReforceDefensePlanet(Planet PlanetOrig, Planet PlanetDest )
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
    // Función que simula una decisión de la IA.
    void MakeDecision()
    {
        // Aquí puedes implementar la lógica de la IA para:
        // - Evaluar la situación actual del juego.
        // - Seleccionar objetivos para atacar.
        // - Mover tropas entre planetas.
        // - Realizar acciones estratégicas, como la conquista de planetas.

        // Ejemplo de una decisión aleatoria (puedes reemplazarlo con una lógica más avanzada):
        GameObject[] enemyPlanets = GameObject.FindGameObjectsWithTag("EnemyPlanet");
        if (enemyPlanets.Length > 0)
        {
            int ObjectiveIndex = Random.Range(0, enemyPlanets.Length);
            // Aquí puedes implementar la lógica para atacar el planeta seleccionado.
        }
    }

    IEnumerator WaitToChangeTurn()
    {
        yield return new WaitForSeconds(waitingTime);
    }
    
}

