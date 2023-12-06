using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] GameObject startMenu;
    [SerializeField] TextMeshProUGUI crownTextStart;
    [SerializeField] TextMeshProUGUI crownTextGame;

    [SerializeField] int initialCrowns = 0;
    [SerializeField] int reward = 10;
    public int boosterCost = 20;
    public int Crowns { get; private set; }

    public void Start()
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
        Crowns -= boosterCost;
        UpdateCrownCounter();
    }
    void UpdateCrownCounter()
    {
        crownTextStart.text = Crowns.ToString();
        crownTextGame.text = Crowns.ToString();
    }
}
