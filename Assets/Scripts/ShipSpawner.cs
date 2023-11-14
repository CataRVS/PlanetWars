using System.Collections;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    // Ajusta la velocidad y otros parámetros según sea necesario.
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

    // Función para generar y mover una nave desde el origen al destino.
    public void SpawnAndMoveShip(GameObject shipPrefab, Vector3 origin, Vector3 destination)
    {
        StartCoroutine(MoveShip(Instantiate(shipPrefab, origin, Quaternion.identity), destination));
    }

    // Rutina para mover gradualmente la nave hacia el destino.
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

