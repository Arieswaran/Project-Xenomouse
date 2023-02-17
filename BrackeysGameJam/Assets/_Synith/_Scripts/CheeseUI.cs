using TMPro;
using UnityEngine;

public class CheeseUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI redText, greenText, blueText;
    [SerializeField] CheeseCounter cheeseCounter;

    void Start()
    {
        cheeseCounter.OnCheeseAmountIncreased += CheeseCounter_OnCheeseAmountIncreased;
        UpdateText();
    }

    void CheeseCounter_OnCheeseAmountIncreased() => UpdateText();

    void OnDestroy()
    {
        cheeseCounter.OnCheeseAmountIncreased -= CheeseCounter_OnCheeseAmountIncreased;
    }

    void UpdateText()
    {
        int redCheeseCount = cheeseCounter.GetRedCheeseCount();
        int greenCheeseCount = cheeseCounter.GetGreenCheeseCount();
        int blueCheeseCount = cheeseCounter.GetBlueCheeseCount();

        redText.SetText(redCheeseCount.ToString());
        greenText.SetText(greenCheeseCount.ToString());
        blueText.SetText(blueCheeseCount.ToString());
    }
}
