using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] float speed = 0.75f; // Set the standart speed of the spaceship
    private float coveredPath = 0.0f;
    private float aumentedPercentage = 0.75f;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    public Vector3 destination;

    private void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (coveredPath < 1f)
        {
            coveredPath += Time.deltaTime * speed;

            // Calcula la posición intermedia usando una función cuadrática (parabólica).
            Vector3 newPosition = Vector3.Lerp(initialPosition, destination, coveredPath);
            float scaleFactor = (Mathf.Sin(coveredPath * (Mathf.PI)) * aumentedPercentage) + 1.0f;

            // Aplica la nueva posición a la nave.
            transform.position = newPosition;
            transform.localScale = initialScale * scaleFactor;
        }
        else
        {
            transform.position = destination;
        }
    }
}
