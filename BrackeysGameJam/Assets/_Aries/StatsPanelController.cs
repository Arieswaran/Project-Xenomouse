using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        healthText.text =  mouseData.health.ToString();
        speedText.text =  mouseData.speed.ToString();
        lifespanText.text =  mouseData.lifespan.ToString();
        hungerBar.fillAmount = 0;
    }

    private void UpdateStats()
    {
        MouseData mouseData = GameManager.instance.GetMouseData();
        AnimationHelper.DoNumberAnimation(healthText,int.Parse(healthText.text), mouseData.health, 0.5f);
        AnimationHelper.DoNumberAnimation(speedText, int.Parse(speedText.text), mouseData.speed, 0.5f);
        AnimationHelper.DoNumberAnimation(lifespanText, int.Parse(lifespanText.text), mouseData.lifespan, 0.5f);
        //AnimationHelper.DoNumberAnimation(actionsText, int.Parse(actionsText.text), mouseData.actions, 0.5f);
        float currentFillAmount = ((float)(mouseData.max_actions - mouseData.actions))/ (float)mouseData.max_actions;
        AnimateProgressBar(currentFillAmount);
    }

    private void AnimateProgressBar(float currentFillAmount)
    {
        hungerBar.DOFillAmount(currentFillAmount, 0.5f);
    }

    private void OnDestroy()
    {
        GameManager.instance.GetMouseData().OnStatsChanged -= UpdateStats;
    }
}
