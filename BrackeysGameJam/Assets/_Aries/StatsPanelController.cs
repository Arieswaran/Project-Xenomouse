using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanelController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI lifespanText;
    public TMPro.TextMeshProUGUI actionsText;
    [SerializeField] private Image hungerBar;

    private void Start()
    {
        GameManager.instance.GetMouseData().OnStatsChanged += UpdateStats;
        MouseData mouseData = GameManager.instance.GetMouseData();
        healthText.text = "Health: " + mouseData.health;
        speedText.text = "Speed: " + mouseData.speed;
        lifespanText.text = "Lifespan: " + mouseData.lifespan;
        actionsText.text = "Actions: " + mouseData.actions;
        hungerBar.fillAmount = 0;
    }

    private void UpdateStats()
    {
        MouseData mouseData = GameManager.instance.GetMouseData();
        
        healthText.text = "Health: " + mouseData.health;
        speedText.text = "Speed: " + mouseData.speed;
        lifespanText.text = "Lifespan: " + mouseData.lifespan;
        actionsText.text = "Actions: " + mouseData.actions;
        hungerBar.fillAmount = ((float)(mouseData.max_actions - mouseData.actions))/ (float)mouseData.max_actions;
    }

    private void OnDestroy()
    {
        GameManager.instance.GetMouseData().OnStatsChanged -= UpdateStats;
    }
}
