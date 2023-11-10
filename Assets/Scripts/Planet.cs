using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string owner; // Variable que almacena al propietario del planeta (player o AI).
    public int capacity; // Variable que almacena la capacidad del planeta (número de tropas que puede contener).
    public int troops; // Variable que almacena el número de tropas que contiene el planeta.

    // Agregar otras variables necesarias para la configuración de tu planeta, como el número de tropas, etc.
    public float elevation = 15.0f; // La cantidad de elevación cuando el ratón pasa por encima.
    private TextMesh troopText; // El TextMesh que muestra el número de tropas en el planeta.
    private Vector3 initialPosition; // La posición inicial del planeta.

    void Start()
    {
        // Almacenar la posición inicial del planeta.
        initialPosition = transform.position;
        TextMesh textMesh = Planet.GetComponentInChildren<TextMesh>();
    }

    void OnMouseEnter()
    {
        // Cuando el ratón entra en el área del planeta, eleva el planeta.
        transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - elevation);
    }

    void OnMouseExit()
    {
        // Cuando el ratón sale del área del planeta, restaura la posición inicial.
        transform.position = initialPosition;
    }

    void OnMouseDown()
    {
        // Cuando el jugador hace clic en el planeta, se llama a la función de conquista del jugador.
        transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - elevation);
        GameObject.Find("player").GetComponent<Player>().ConquerPlanet(this, this);
    }
    public void UpdateColorTextMesh()
    {
        // Actualiza color del planeta.
        if (owner = "player")
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