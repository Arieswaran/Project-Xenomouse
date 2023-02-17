using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMPTimer : MonoBehaviour
{
    [SerializeField] Image uiFill;
    [SerializeField] TextMeshProUGUI uiText;
    Player player;

    int Duration;

    private void Start()
    {
        player = MazePhaseManager.Instance.GetPlayer();
        Duration = player.GetLifeSpan();
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
        uiText.text = $"{lifespan / 60:00}:{lifespan % 60:00}";
        uiFill.fillAmount = Mathf.InverseLerp(0, Duration, lifespan);

        if (lifespan <= 0)
        {
            OnEnd();
        }
    }

    private void OnEnd()
    {
        print("End");
    }
}
