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
        //need to decrease cheese count , but have to make support for multiple cheese types
        DoCommonThingsAfterAction();
    }

    private void DoCommonThingsAfterAction(){ 
        overlay.SetActive(true); // To prevent multiple animation triggers from overlapping
        if(GameManager.instance.CanDoActions())
            Invoke("DisableOverlay", 1.6f); // Disable overlay after 1.5 seconds
        else{
            // Load Maze Scene
            GameManager.instance.LoadMazeScene();
        }
    }

    private void DisableOverlay(){
        overlay.SetActive(false);
    }


}
