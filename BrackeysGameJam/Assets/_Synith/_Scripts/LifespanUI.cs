using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifespanUI : MonoBehaviour
{
    Player player;
    [SerializeField] TextMeshProUGUI lifespanText;

    void Start()
    {
        player = MazePhaseManager.Instance.GetPlayer();
        player.OnLifespanChanged += Player_OnLifespanLost;
        UpdateText(player.GetLifespan());
    }

    void Player_OnLifespanLost(int lifespanRemaining)
    {
        UpdateText(lifespanRemaining);
    }

    void UpdateText(int lifespan)
    {
        lifespanText.SetText(lifespan.ToString());
    }
}
