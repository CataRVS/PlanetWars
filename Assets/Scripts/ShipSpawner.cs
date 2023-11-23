using System.Collections;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    // private Transform SpawnerPosition;
    [SerializeField] GameObject playerShipPrefab;
    [SerializeField] GameObject enemyShipPrefab;
    private Transform spawner;
    private string owner;

    void Start()
    {
        Planet planet = GetComponent<Planet>();
        owner = planet.owner;
        spawner = planet.transform.Find("ShipSpawner");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnShip(new Vector3(0, 0, 9.99f));
        }
    }
    private void SpawnShip(Vector3 destination)
    {
        GameObject spaceshipRaw;
        if (owner == "player")
        {
            spaceshipRaw = Instantiate(playerShipPrefab, spawner.position, Quaternion.identity);
            //spaceshipRaw.transform.rotation = Quaternion.Euler(-20f, 90f, -25f);
        }
        else
        {
            spaceshipRaw = Instantiate(enemyShipPrefab, spawner.position, Quaternion.identity);
            //spaceshipRaw.transform.rotation = Quaternion.Euler(20f, -90f, 25f);
        }
        spaceshipRaw.transform.rotation = Quaternion.Euler(CalculateRotation(destination));
        Spaceship spaceship = spaceshipRaw.GetComponent<Spaceship>();
        spaceship.destination = destination;
    }

    private Vector3 CalculateRotation(Vector3 destination)
    {
        Vector3 origin = spawner.position;
        float xRotation;
        float yRotation;
        float zRotation;
        float xRotationRad = Mathf.Atan2((destination.y - origin.y), (destination.x - origin.x));
        if (origin.x <= destination.x)
        {
            xRotation = -xRotationRad * Mathf.Rad2Deg;
            yRotation = 90.0f;
            zRotation = -25.0f;
        }
        else
        {
            xRotation = 180 + xRotationRad * Mathf.Rad2Deg;
            yRotation = -90.0f;
            zRotation = 25.0f;
        }
        Vector3 Rotation = new(xRotation, yRotation, zRotation);
        return Rotation;
    }
}
