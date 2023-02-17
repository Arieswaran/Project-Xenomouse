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
        UpdateText(player.GetLifeSpan());
    }

    void OnDestroy()
    {
        player.OnLifespanChanged -= Player_OnLifespanLost;
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
