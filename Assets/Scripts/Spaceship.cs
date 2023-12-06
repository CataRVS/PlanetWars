using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    // Set the standart speed of the spaceship
    [SerializeField] float speed = 0.75f;
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

            // Calcula la posición intermedia usando una función cuadrática (parabólica).
            Vector3 newPosition = Vector3.Lerp(initialPosition, destination, coveredPath);
            //newPosition.z = Mathf.Sin(coveredPath * (-Mathf.PI)) * 4f + 9.99f;

            // Aplica la nueva posición a la nave.
            transform.position = newPosition;
        }
        else
        {
            transform.position = destination;
        }
    }
}
