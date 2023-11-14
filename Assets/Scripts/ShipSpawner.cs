using System.Collections;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    // Set the standart speed of the spaceship
    public float shipSpeed = 1.0f;
    // private Transform SpawnerPosition;
    [SerializeField] Transform SpawnerPosition;
    [SerializeField] GameObject playerShipPrefab;
    [SerializeField] GameObject enemyShipPrefab;
    Planet planet;

    void Start()
    {
        planet = GetComponent<Planet>();
        // if (planet != null)
        // {
        //     SpawnerPosition = planet.shipSpawner.transform;
        // }
        // else
        // {
        //     Debug.Log("No hay planeta");
        // }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnShip();
        }
    }
    private void SpawnShip()
    {
        GameObject spaceShip = null;
        if (planet.owner == "player")
        {
            spaceShip = Instantiate(playerShipPrefab, SpawnerPosition.position, Quaternion.identity);
            spaceShip.transform.rotation = Quaternion.Euler(-20f, 90f, -25f);
        }
        else
        {
            spaceShip = Instantiate(enemyShipPrefab, SpawnerPosition.position, Quaternion.identity);
            spaceShip.transform.rotation = Quaternion.Euler(20f, -90f, 25f);
        }
        StartCoroutine(MoveShipParabolically(spaceShip, Vector3.zero));
    }

    // Generates and moves the the spaceship from an origin to a destination
    private IEnumerator MoveShipParabolically(GameObject spaceShip, Vector3 destination)
    {
        float coveredPath = 0f;
        Vector3 initialPosition = SpawnerPosition.transform.position;
        Debug.Log(initialPosition);

        while (coveredPath < 1f)
        {
            coveredPath += Time.deltaTime * shipSpeed;

            // Calcula la posición intermedia usando una función cuadrática (parabólica).
            Vector3 newPosition = Vector3.Lerp(initialPosition, destination, coveredPath);
            newPosition.y = Mathf.Sin(coveredPath * Mathf.PI) * 2f;

            // Aplica la nueva posición a la nave.
            spaceShip.transform.position = newPosition;

            yield return null;
        }
        spaceShip.transform.position = destination;
    }
}
