using System;
using UnityEngine;

public class CheeseCounter : MonoBehaviour
{
    int redCheese;
    int greenCheese;
    int blueCheese;

    public event Action OnCheeseAmountIncreased;

    public int GetRedCheeseCount() => redCheese;
    public int GetGreenCheeseCount() => greenCheese;
    public int GetBlueCheeseCount() => blueCheese;

    void Start()
    {
        Cheese.OnAnyCheeseConsumed += Cheese_OnAnyCheeseConsumed;
    }

    private void OnDestroy()
    {
        Cheese.OnAnyCheeseConsumed -= Cheese_OnAnyCheeseConsumed;
    }

    void Cheese_OnAnyCheeseConsumed(object sender, EventArgs e)
    {
        Cheese cheese = sender as Cheese;

        switch (cheese.cheeseType)
        {
            case Cheese.CheeseType.Red:
                MazePhaseManager.Instance.IncreaseRedCheeseCount();
                redCheese++;
                break;
            case Cheese.CheeseType.Green:
                MazePhaseManager.Instance.IncreaseGreenCheeseCount();
                greenCheese++;
                break;
            case Cheese.CheeseType.Blue:
                MazePhaseManager.Instance.IncreaseBlueCheeseCount();
                blueCheese++;
                break;
        }
        OnCheeseAmountIncreased?.Invoke();
        Debug.Log($"Red: {redCheese} Green: {greenCheese} Blue: {blueCheese}");
    }
}
