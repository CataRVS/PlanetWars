using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetInitializer : MonoBehaviour
{
    List<Planet> planetList = new List<Planet>();  // Una lista de planetas en tu escena.

    void Awake()
    {
        
        Planet[] planetsArray = GameObject.FindObjectsOfType<Planet>();

        // Agrega los planetas encontrados a la lista.
        planetList.AddRange(planetsArray);

        // Puedes imprimir la cantidad de planetas encontrados para verificar.
        Debug.Log("Número de planetas en la lista: " + planetList.Count);

        // Ahora, planetsList contiene todos los planetas de la escena.
        int totalPlanets = planetList.Count;
        int half = totalPlanets / 2;

        for (int i = 0; i < totalPlanets; i++)
        {
            if (i < half)
            {
                // Asigna el planeta al jugador (cambia "Planeta" por el nombre del componente o script que almacena el dueño del planeta).
                planetList[i].owner = "AI";
                // Asignar el color del text mesh azul 
                ChangeTextMeshColor(planetList[i], Color.red);
            }
            else
            {
                // Asigna el planeta a la IA.
                planetList[i].owner = "player";
                // Asignar el color del text mesh rojo
                ChangeTextMeshColor(planetList[i], Color.blue);
            }
        }
    }
    void ChangeTextMeshColor(Planet planet, Color color)
    {
        TextMeshPro planetText = planet.GetComponentInChildren<TextMeshPro>();
        if (planetText != null)
        {
            planetText.color = color;
        }
    }
}

