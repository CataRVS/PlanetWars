using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInitializer : MonoBehaviour
{
    public GameObject[] planets;  // Una lista de planetas en tu escena.

    void Start()
    {
        int totalPlanets = planets.Length;
        int half = totalPlanets / 2;

        for (int i = 0; i < totalPlanets; i++)
        {
            if (i < half)
            {
                // Asigna el planeta al jugador (cambia "Planeta" por el nombre del componente o script que almacena el dueño del planeta).
                planets[i].GetComponent<Planet>().owner = "player";
                // Asignar el color del text mesh azul 
                ChangeTextMeshColor(planets[i].GetComponent<Planet>(), Resources.Load<Material>("Blue"));
            }
            else
            {
                // Asigna el planeta a la IA.
                planets[i].GetComponent<Planet>().owner = "AI";
                // Asignar el color del text mesh rojo
                ChangeTextMeshColor(planets[i].GetComponent<Planet>(), Resources.Load<Material>("Red"));
            }
        }
    }
    void ChangeTextMeshColor(Planet planet, Material material)
    {
        TextMesh textMesh = planet.GetComponentInChildren<TextMesh>();
        if (textMesh != null)
        {
            textMesh.GetComponent<Renderer>().material = material;
        }
    }
}

