using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;

    [SerializeField] GameObject game;
    [SerializeField] GameObject startMenu;
    [SerializeField] TextMeshProUGUI crownTextStart;
    [SerializeField] TextMeshProUGUI crownTextGame;

    [SerializeField] int initialCrowns = 0;
    [SerializeField] int reward = 10;
    public int BoosterCost { get; private set; } = 20;
    public int Crowns { get; private set; }

    // Public property to access the singleton instance
    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            // If the instance is null, try to find it in the scene
            _instance = FindObjectOfType<GameManager>();

            // If it's still null, create a new GameObject and add the GameManager script to it
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("GameManager");
                _instance = singletonObject.AddComponent<GameManager>();
            }
        }
        return _instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Crowns = initialCrowns;
        startMenu.SetActive(true);
        game.SetActive(false);
        Time.timeScale = 1;
        UpdateCrownCounter();
    }

    public void AddCrowns()
    {
        Crowns += reward;
        UpdateCrownCounter();
    }

    public void RemoveCrowns()
    {
        Crowns -= BoosterCost;
        UpdateCrownCounter();
    }

    private void UpdateCrownCounter()
    {
        crownTextStart.text = Crowns.ToString();
        crownTextGame.text = Crowns.ToString();
    }
}
