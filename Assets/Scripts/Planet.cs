using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string owner; // Variable que almacena al propietario del planeta (player o AI).
    public int capacity; // Variable que almacena la capacidad del planeta (número de tropas que puede contener).
    public int troops; // Variable que almacena el número de tropas que contiene el planeta.

    // Agregar otras variables necesarias para la configuración de tu planeta, como el número de tropas, etc.
    private TextMesh troopText; // El TextMesh que muestra el número de tropas en el planeta.
    private Vector3 initialPosition; // La posición inicial del planeta.
    private Light spotLight; // La luz que se muestra cuando el planeta es seleccionado.
    private Player player; // Referencia al script Player.
    private bool clickEnabled = true; // Variable que habilita o deshabilita el clic en el planeta.

    void Start()
    {
        // Almacenar la posición inicial del planeta.
        initialPosition = transform.position;
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        spotLight = GetComponentInChildren<Light>();
        if (spotLight != null)
        {
            spotLight.enabled = false; // Desactiva el spotlight al inicio.
        }
        player = FindObjectOfType<Player>();
    }

    void OnMouseEnter()
    {
        // Cuando el ratón entra en el área del planeta, eleva el planeta.
        if (spotLight != null)
        {
            spotLight.enabled = true; // Activa el halo.
        }
    }

    void OnMouseExit()
    {
        // Cuando el ratón sale del área del planeta, restaura la posición inicial.
        if (spotLight != null)
        {
            spotLight.enabled = false; // Desactiva el halo.
        }
    }

    void OnMouseDown()
    {
        if (clickEnabled)
        {
            player.DetectClickedPlanet();
            StartCoroutine(EnableClickAfterDelay()); // Habilita los clics después de un cierto tiempo.
        }
        // Cuando el jugador hace clic en el planeta, se llama a la función de conquista del jugador.
        //transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - elevation);
       
    }
    IEnumerator EnableClickAfterDelay()
    {
        clickEnabled = false; // Deshabilita los clics temporalmente.
        yield return new WaitForSeconds(1.0f); // Ajusta el tiempo según tus necesidades.
        clickEnabled = true; // Habilita los clics después del tiempo especificado.
    }

    public void UpdateColorTextMesh()
    {
        // Actualiza color del planeta.
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