using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCounterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;

    void Start()
    {
        if (GameManager.instance == null) return;
        int generationCount = GameManager.instance.GetMouseData().generation_count;
        roundText.text = $"#{generationCount}";
    }
}
