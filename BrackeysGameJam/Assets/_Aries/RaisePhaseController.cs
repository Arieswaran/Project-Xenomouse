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
    [SerializeField] private Button logButton;
    [SerializeField] private JournalPopupController journalPopupController;
    [SerializeField] private GameObject sendToMazeButtonGameObject;
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
            logButton.gameObject.SetActive(true);
        }else{
            choicePanelController.gameObject.SetActive(false);
            logButton.gameObject.SetActive(false);
        }
        currentScale = mouse.localScale.x;
        SetButtons();
    }

    private void SetButtons(){
        logButton.onClick.RemoveAllListeners();
        logButton.onClick.AddListener(() => {
            journalPopupController.gameObject.SetActive(true);
        });
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
        Invoke("DisableOverlay", 1.6f); // Disable overlay after 1.5 seconds
        if(!GameManager.instance.CanDoActions()){
            sendToMazeButtonGameObject.SetActive(true);
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
