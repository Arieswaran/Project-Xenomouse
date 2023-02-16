using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelController : MonoBehaviour
{
    [SerializeField] private Button FeedButton, BrushButton, PlayButton, FeedCheeseButton , FeedPelletButton , SendToMazeButton , ViewJournalButton , RedCheeseButton, BlueCheeseButton, GreenCheeseButton;

    [SerializeField] private List<GameObject> feedOptions, normalOptions;
    [SerializeField] private GameObject all_buttons_parent, cheese_buttons_parent;

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
            ToggleCheeseButtons();
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
        RedCheeseButton.onClick.RemoveAllListeners();
        RedCheeseButton.onClick.AddListener(() => {
            raisePhaseController.TriggerEating();
            GameManager.instance.IncreaseMouseHealth(10);
            GameManager.instance.IncreaseRedCheese(-1);
            ToggleCheeseButtons();
            CheckCheeseAvailability();
        });
        BlueCheeseButton.onClick.RemoveAllListeners();
        BlueCheeseButton.onClick.AddListener(() => {
            raisePhaseController.TriggerEating();
            GameManager.instance.IncreaseMouseSpeed(10);
            GameManager.instance.IncreaseBlueCheese(-1);
            ToggleCheeseButtons();
            CheckCheeseAvailability();
        });
        GreenCheeseButton.onClick.RemoveAllListeners();
        GreenCheeseButton.onClick.AddListener(() => {
            raisePhaseController.TriggerEating();
            GameManager.instance.IncreaseMouseLifespan(20);
            GameManager.instance.IncreaseGreenCheese(-1);
            ToggleCheeseButtons();
            CheckCheeseAvailability();
        });
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

    private void ToggleCheeseButtons(){
        cheese_buttons_parent.SetActive(!cheese_buttons_parent.activeSelf);
        all_buttons_parent.SetActive(!all_buttons_parent.activeSelf);
    }

    private void CheckCheeseAvailability(){
        int red_cheese_count = GameManager.instance.GetPlayerData().red_cheese_count;
        int blue_cheese_count = GameManager.instance.GetPlayerData().blue_cheese_count;
        int green_cheese_count = GameManager.instance.GetPlayerData().green_cheese_count;
        FeedCheeseButton.interactable = red_cheese_count > 0 || blue_cheese_count > 0 || green_cheese_count > 0;
        RedCheeseButton.interactable = red_cheese_count > 0;
        BlueCheeseButton.interactable = blue_cheese_count > 0;
        GreenCheeseButton.interactable = green_cheese_count > 0;
    }
}
