using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RaisePhaseController : MonoBehaviour
{
    [SerializeField] private Animator _mouseAnimator;

    [SerializeField] private GameObject overlay;

    public static RaisePhaseController instance;

    [SerializeField] private ChoicePanelController choicePanelController;
    [SerializeField] private Transform mouse;
    private float currentScale = 1f;
    private float scaleChange = 1.1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start() {
        int generation_count = GameManager.instance.GetGenerationCount();
        if (generation_count >= 1)
        {
            choicePanelController.gameObject.SetActive(true);
        }
        currentScale = mouse.localScale.x;
    }

    // Animation states "Playing" "Brushing" "Eating"
    public void TriggerPlaying(){
        _mouseAnimator.SetTrigger("Playing");
        GameManager.instance.IncreasePlayedCount();
        DoCommonThingsAfterAction();
    }

    public void TriggerBrushing(){
        _mouseAnimator.SetTrigger("Brushing");
        GameManager.instance.IncreaseBrushedCount();
        DoCommonThingsAfterAction();
    }

    public void TriggerEating(){
        _mouseAnimator.SetTrigger("Eating");
        GameManager.instance.DecreaseActions();
        Invoke(nameof(IncreaseMouseScale), 1.5f);
        DoCommonThingsAfterAction(2.1f);
    }

    public void TriggerRevive(){
        _mouseAnimator.SetTrigger("Revive");
        DoCommonThingsAfterAction();
    }

    private void DoCommonThingsAfterAction(float delay = 1.6f){ 
        overlay.SetActive(true); // To prevent multiple animation triggers from overlapping
        if(GameManager.instance.CanDoActions())
            Invoke("DisableOverlay", 1.6f); // Disable overlay after 1.5 seconds
        else{
            // Load Maze Scene
            GameManager.instance.LoadMazeScene();
        }
    }

    private void IncreaseMouseScale(){
        mouse.DOScale(currentScale * scaleChange, 0.5f);
        currentScale *= scaleChange;
    }

    private void DisableOverlay(){
        overlay.SetActive(false);
    }


}
