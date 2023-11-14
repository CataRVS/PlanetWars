using System.Collections;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    // Set the standart speed of the spaceship
    public float shipSpeed = 5.0f;
    [SerializeField] Transform SpawnerPosition;
    [SerializeField] GameObject playerShipPrefab;
    [SerializeField] GameObject enemyShipPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnShip();
        }
    }
    private void SpawnShip()
    {
        Instantiate(playerShipPrefab, SpawnerPosition.position, Quaternion.identity);
    }

    // Generates and moves the the spaceship from an origin to a destination
    public void SpawnAndMoveShip(GameObject shipPrefab, Vector3 origin, Vector3 destination)
    {
        StartCoroutine(MoveShip(Instantiate(shipPrefab, origin, Quaternion.identity), destination));
    }

    // Routine to move gradually the spaceship from the origin to the destination
    IEnumerator MoveShip(GameObject ship, Vector3 destination)
    {
        while (Vector3.Distance(ship.transform.position, destination) > 0.1f)
        {
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, destination, shipSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(ship);
    }
}

