using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanelController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI lifespanText;
    public TMPro.TextMeshProUGUI actionsText;

    private void Start()
    {
        GameManager.instance.GetMouseData().OnStatsChanged += UpdateStats;
        UpdateStats();
    }

    private void UpdateStats()
    {
        Debug.Log("Updating Stats");
        MouseData mouseData = GameManager.instance.GetMouseData();
        healthText.text = "Health: " + mouseData.health;
        speedText.text = "Speed: " + mouseData.speed;
        lifespanText.text = "Lifespan: " + mouseData.lifespan;
        actionsText.text = "Actions: " + mouseData.actions;
    }

    private void OnDestroy()
    {
        GameManager.instance.GetMouseData().OnStatsChanged -= UpdateStats;
    }
}
