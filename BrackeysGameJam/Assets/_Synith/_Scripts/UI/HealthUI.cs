using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    Player player;
    [SerializeField] TextMeshProUGUI healthText;

    void Start()
    {
        player = MazePhaseManager.Instance.GetPlayer();
        player.OnHealthAmountChanged += Player_OnHealthLost;
        UpdateText(player.GetHealth());
    }

    void OnDestroy()
    {
        player.OnHealthAmountChanged -= Player_OnHealthLost;
    }

    void Player_OnHealthLost(int healthAmountRemaining)
    {
        UpdateText(healthAmountRemaining);
    }

    void UpdateText(int health)
    {
        healthText.SetText(health.ToString());
    }
}
