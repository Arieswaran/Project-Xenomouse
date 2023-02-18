using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    Player player;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Image fillImage;

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
        if (health < 0)
        {
            health = 0;
        }

         healthText.SetText($"HP  {health}");

        float healthNormalized = (float)health / player.GetMaxHealth();
        fillImage.fillAmount = healthNormalized;
    }
}
