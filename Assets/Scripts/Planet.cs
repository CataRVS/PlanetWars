using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] GameObject playerShipPrefab;
    [SerializeField] GameObject enemyShipPrefab;
    [SerializeField] GameObject shipManager;

    [SerializeField] int sendTroops = 5; // Amout of troops sent by default
    [SerializeField] int initialTroops = 12;
    [SerializeField] int initialCapacity = 25;
    [SerializeField] float initialRegenerationTime = 2.0f;
    [SerializeField] float regenerationTime; // Time needed to add one troop

    [SerializeField] TextMeshPro troopText; // Shows the number of troops and the capacity of the planet
    [SerializeField] Light spotLight; // The light shown when the planet is selected
    [SerializeField] GameObject spawnerGameObject;

    private Transform spawner; // Referencia al spawner.
    public string owner = ""; // Saves the owner of the planet (player o AI)
    public int Capacity { get; private set; } // Saves the capacity of the planet
    public int Troops { get; private set; } // Saves the number of troops in the planet

    private void OnEnable()
    {
        troopText.gameObject.SetActive(true);
        spotLight.gameObject.SetActive(true);
        spotLight.enabled = false;
        spawnerGameObject.SetActive(true);
        spawner = spawnerGameObject.transform;
        StartPlanet();
    }

    private void StartPlanet()
    {
        Troops = initialTroops;
        Capacity = initialCapacity;
        regenerationTime = initialRegenerationTime;
        UpdateTextMesh();
        StartCoroutine(RegenerateTroops());
    }

    private void OnMouseEnter()
    {
        // When the mouse enters the planet, it glows
        if (spotLight != null)
        {
            spotLight.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        // When the mouse exits the planet, it stops glowing
        if (spotLight != null)
        {
            spotLight.enabled = false;
        }
    }

    public void SendSpaceship(UnityEngine.Vector3 destination)
    {
        if (Troops > sendTroops)
        {
            Troops -= 5;
            GameObject spaceshipRaw;
            if (owner == "player")
            {
                spaceshipRaw = Instantiate(playerShipPrefab, spawner.position, UnityEngine.Quaternion.identity);
            }
            else
            {
                spaceshipRaw = Instantiate(enemyShipPrefab, spawner.position, UnityEngine.Quaternion.identity);
            }
            // spaceshipRaw is a child of shipManager
            spaceshipRaw.transform.parent = shipManager.transform;
            spaceshipRaw.transform.rotation = UnityEngine.Quaternion.Euler(CalculateRotation(destination));
            Spaceship spaceship = spaceshipRaw.GetComponent<Spaceship>();
            spaceship.destination = destination;
            UpdateTextMesh();
        }
    }

    private UnityEngine.Vector3 CalculateRotation(UnityEngine.Vector3 destination)
    {
        // This function calculates the inclination of the spaceship and it's direction depending on it's destination.
        UnityEngine.Vector3 origin = spawner.position;
        float xRotation;
        float yRotation;
        float zRotation;
        // We calculate the inclination of the spaceship in radians
        float xRotationRad = Mathf.Atan2((destination.y - origin.y), (destination.x - origin.x));
        if (origin.x <= destination.x)
        {
            xRotation = -xRotationRad * Mathf.Rad2Deg; // rotation in degrees
            yRotation = 90.0f;
            zRotation = -25.0f;
        }
        else
        {
            xRotation = 180 + xRotationRad * Mathf.Rad2Deg; // rotation in degrees
            yRotation = -90.0f;
            zRotation = 25.0f;
        }
        UnityEngine.Vector3 Rotation = new(xRotation, yRotation, zRotation);
        return Rotation;
    }
    //
    private void OnTriggerEnter(Collider collision) // When a spaceship arrives to the planet
    {
        GameObject spaceship = collision.gameObject;
        if (owner == "player")
        {
            if (spaceship.CompareTag("AI"))
            {
                if (Troops < sendTroops)
                {
                    owner = "AI";
                    Troops = sendTroops - Troops;
                    UpdateColorTextMesh();
                }
                // If there aren't enough troops, we don't conquer
                else
                {
                    Troops -= sendTroops;
                }
            }
            else if (spaceship.CompareTag("Player"))
            {
                // If we don't reach the capacity with the actual number of troops sent, we send them all
                if (Troops + sendTroops <= Capacity)
                {
                    Troops += sendTroops;
                }
                // If we reach the capacity with the actual number of troops sent, we only send the ones needed to reach the capacity
                else
                {
                    Troops = Capacity;
                }
            }
        }
        else
        {
            if (spaceship.CompareTag("AI"))
            {
                // If we don't reach the capacity with the actual number of troops sent, we send them all
                if (Troops + sendTroops <= Capacity)
                {
                    Troops += sendTroops;
                }
                // If we reach the capacity with the actual number of troops sent, we only send the ones needed to reach the capacity
                else
                {
                    Troops = Capacity;
                }
            }
            else if (spaceship.CompareTag("Player"))
            {
                if (Troops < sendTroops)
                {
                    owner = "player";
                    Troops = sendTroops - Troops;
                    UpdateColorTextMesh();
                }
                // If there aren't enough troops, we don't conquer
                else
                {
                    Troops -= sendTroops;
                }
            }
        }
        Destroy(spaceship);
        UpdateTextMesh();
    }

    private IEnumerator RegenerateTroops()
    {
        float timer = 0f;
        while (true)
        {
            // Adds the time difference
            timer += Time.deltaTime;

            // If the regeneration time has passed we restart the timer and add a troop
            if (timer >= regenerationTime)
            {
                timer = 0f;
                if (Troops < Capacity)
                {
                    Troops++;
                    UpdateTextMesh();
                }
            }
            yield return null;
        }
    }

    public void UpdateColorTextMesh()
    {
        // The planet is blue if the owner is the player, otherwise it's red.
        if (owner == "player")
        {
            troopText.color = Color.blue;
        }
        else
        {
            troopText.color = Color.red;
        }
    }

    private void UpdateTextMesh()
    {
        if (troopText != null)
        {
            troopText.text = $"{Troops}/{Capacity}";
        }
    }

    public void UpgradeRegenerationTime()
    {
        regenerationTime /= 2;
    }
}