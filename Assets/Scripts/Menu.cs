using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject Introduccion;
    private GameObject Game;
    private Vector3 normalScale;
    public float scaleFactor = 1.15f;

    private void Start()
    {
        normalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Ajustar la escala para hacer el botón un poco más grande.
        transform.localScale = normalScale * scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaurar la escala normal cuando el ratón sale del botón.
        transform.localScale = normalScale;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}

