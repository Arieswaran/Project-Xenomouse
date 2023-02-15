using System;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public static event EventHandler OnAnyCheeseConsumed;

    public enum CheeseType
    {
        Red = 10,
        Blue = 20,
        Green = 30,
    }

    public void Consume()
    {
        OnAnyCheeseConsumed?.Invoke(this, EventArgs.Empty);
    }


    public CheeseType cheeseType;
}
