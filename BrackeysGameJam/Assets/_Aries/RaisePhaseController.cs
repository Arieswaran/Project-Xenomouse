using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaisePhaseController : MonoBehaviour
{
    [SerializeField] private Animator _mouseAnimator;

    [SerializeField] private GameObject overlay;

    public static RaisePhaseController instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {

    }

    // Animation states "Playing" "Brushing" "Eating"
    public void TriggerPlaying(){
        _mouseAnimator.SetTrigger("Playing");
        GameManager.instance.IncreaseMouseSpeed(1);
        DoCommonThingsAfterAction();
    }

    public void TriggerBrushing(){
        _mouseAnimator.SetTrigger("Brushing");
        GameManager.instance.IncreaseMouseHealth(1);
        DoCommonThingsAfterAction();
    }

    public void TriggerEating(){
        _mouseAnimator.SetTrigger("Eating");
        GameManager.instance.IncreaseMouseHealth(1);
        DoCommonThingsAfterAction();
    }

    private void DoCommonThingsAfterAction(){ 
        overlay.SetActive(true); // To prevent multiple animation triggers from overlapping
        GameManager.instance.IncreaseMouseLifespan(-1); // cause I am considering the lifespan as the number of actions the mouse can do
        if(GameManager.instance.CanDoActions())
            Invoke("DisableOverlay", 1.5f); // Disable overlay after 1.5 seconds
    }

    private void DisableOverlay(){
        overlay.SetActive(false);
    }


}
