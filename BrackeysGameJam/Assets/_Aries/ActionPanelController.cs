using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : MonoBehaviour
{
    [SerializeField] private Button FeedButton, BrushButton, PlayButton, FeedCheeseButton , FeedPelletButton , SendToMazeButton , ViewJournalButton;

    [SerializeField] private List<GameObject> feedOptions, normalOptions;

    [SerializeField] private RaisePhaseController raisePhaseController;

    private void Start() {
        SetButtons();
    }

    private void SetButtons(){
        FeedButton.onClick.RemoveAllListeners();
        FeedButton.onClick.AddListener(() => {
            ToggleFeedOptions();
        });
        BrushButton.onClick.RemoveAllListeners();
        BrushButton.onClick.AddListener(() => {
            raisePhaseController.TriggerBrushing();
        });
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(() => {
            raisePhaseController.TriggerPlaying();
        });
        FeedCheeseButton.onClick.RemoveAllListeners();
        FeedCheeseButton.onClick.AddListener(() => {
            raisePhaseController.TriggerEating();
            GameManager.instance.IncreaseMouseHealth(10);
            ToggleFeedOptions();
        });
        FeedPelletButton.onClick.RemoveAllListeners();
        FeedPelletButton.onClick.AddListener(() => {
            raisePhaseController.TriggerEating();
            GameManager.instance.IncreaseMouseHealth(1);
            ToggleFeedOptions();
        });
        // SendToMazeButton.onClick.RemoveAllListeners();
        // SendToMazeButton.onClick.AddListener(() => {
        //     // Load Maze Scene
        // });
        // ViewJournalButton.onClick.RemoveAllListeners();
        // ViewJournalButton.onClick.AddListener(() => {
        //     // Show Journal
        // });
    }

    private void ToggleFeedOptions(){
        foreach(GameObject option in feedOptions){
            option.SetActive(!option.activeSelf);
        }
        foreach(GameObject option in normalOptions){
            option.SetActive(!option.activeSelf);
        }
        CheckCheeseAvailability();
    }

    private void CheckCheeseAvailability(){
        if(GameManager.instance.GetPlayerData().red_cheese_count == 0 && GameManager.instance.GetPlayerData().blue_cheese_count == 0 && GameManager.instance.GetPlayerData().green_cheese_count == 0){
            FeedCheeseButton.interactable = false;
        }else{
            FeedCheeseButton.interactable = true;
        }
    }
}
