using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    // Set the standart speed of the spaceship
    [SerializeField] float speed = 1.0f;
    private float coveredPath = 0.0f;
    private Vector3 initialPosition;
    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (coveredPath < 1f)
        {
            coveredPath += Time.deltaTime * speed;

            // Calcula la posici�n intermedia usando una funci�n cuadr�tica (parab�lica).
            Vector3 newPosition = Vector3.Lerp(initialPosition, destination, coveredPath);
            //newPosition.z = Mathf.Sin(coveredPath * (-Mathf.PI)) * 4f + 9.99f;

            // Aplica la nueva posici�n a la nave.
            transform.position = newPosition;
        }
        else
        {
            transform.position = destination;
        }
    }

    // Generates and moves the the spaceship from an origin to a destination
    //public IEnumerator MoveShipParabolically(Vector3 destination)
    //{
    //    float coveredPath = 0f;
    //    Vector3 initialPosition = transform.position;
    //    Debug.Log(initialPosition);

    //    while (coveredPath < 1f)
    //    {
    //        coveredPath += Time.deltaTime * speed;

    //        // Calcula la posici�n intermedia usando una funci�n cuadr�tica (parab�lica).
    //        Vector3 newPosition = Vector3.Lerp(initialPosition, destination, coveredPath);
    //        newPosition.y = Mathf.Sin(coveredPath * Mathf.PI) * 2f;

    //        // Aplica la nueva posici�n a la nave.
    //        transform.position = newPosition;

    //        yield return null;
    //    }
    //    transform.position = destination;
    //}
}