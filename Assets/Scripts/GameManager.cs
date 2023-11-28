using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Planet> planetList = new List<Planet>();  // Una lista de planetas en tu escena.
    // Start is called before the first frame update
    void Start()
    {
        Planet[] planetsArray = FindObjectsOfType<Planet>();
        int totalPlanets = planetsArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
