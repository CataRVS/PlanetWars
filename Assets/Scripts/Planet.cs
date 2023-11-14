using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] int initialTroops = 12;
    [SerializeField] int initialCapacity = 25;
    [SerializeField] int sendTroops = 5; // Amout of troops sent by default

    public string owner = ""; // Saves the owner of the planet (player o AI).
    public int capacity; // Saves the capacity of the planet
    public int troops; // Saves the number of troops in the planet

    private TextMeshPro troopText; // Shows the number of troops and the capacity of the planet
    private Light spotLight; // The light shown when the planet is selected
    private ShipSpawner shipSpawner; // Referencia al spawner.

    void Start()
    {
        // We look for the GameObjects corresponding to each variable
        troopText = GetComponentInChildren<TextMeshPro>();
        spotLight = GetComponentInChildren<Light>();
        shipSpawner = GameObject.FindObjectOfType<ShipSpawner>();
    }
    void Awake()
    {
        // We look for the GameObjects corresponding to each variable
        troopText = GetComponentInChildren<TextMeshPro>();
        spotLight = GetComponentInChildren<Light>();
        // We nable the spotlight at the beginig of each game.
        spotLight.enabled = false;

        troops = initialTroops;
        capacity = initialCapacity;
        UpdateTextMesh();
    }
    void OnMouseEnter()
    {
        // When the mouse enters the planet, it glows
        if (spotLight != null)
        {
            spotLight.enabled = true;
        }
    }

    void OnMouseExit()
    {
        // When the mouse exits the planet, it stops glowing
        if (spotLight != null)
        {
            spotLight.enabled = false;
        }
    }

    void OnMouseDown()
    {
        // We don't do anything for the moment
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject spaceShip = collision.gameObject;
        if (owner == "player")
        {
            if (spaceShip.CompareTag("AI"))
            {
                if (troops < sendTroops)
                {
                owner = "AI";
                troops = sendTroops - troops;
                UpdateColorTextMesh();
                }
                // If there aren't enough troops, we don't conquer
                else
                {
                    troops = troops - sendTroops;
                }
            }
            else if (spaceShip.CompareTag("Player"))
            {
                // If we don't reach the capacity with the actual number of troops sent, we send them all
                if (troops + sendTroops <= capacity)
                {
                    troops = troops + sendTroops;
                }
                // If we reach the capacity with the actual number of troops sent, we only send the ones needed to reach the capacity
                else
                {
                    troops = capacity;
                }
            }
        }
        else
        {
            if (spaceShip.CompareTag("AI"))
            {
                // If we don't reach the capacity with the actual number of troops sent, we send them all
                if (troops + sendTroops <= capacity)
                {
                    troops = troops + sendTroops;
                }
                // If we reach the capacity with the actual number of troops sent, we only send the ones needed to reach the capacity
                else
                {
                    troops = capacity;
                }
            }
            else if (spaceShip.CompareTag("Player"))
            {
                if (troops < sendTroops)
                {
                owner = "player";
                troops = sendTroops - troops;
                UpdateColorTextMesh();
                }
                // If there aren't enough troops, we don't conquer
                else
                {
                    troops = troops - sendTroops;
                }
            }
        }
        Destroy(spaceShip);
        UpdateTextMesh();
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

    public void UpdateTextMesh()
    {
        if (troopText != null)
        {
            troopText.text = $"{troops}/{capacity}";
        }
    }
}