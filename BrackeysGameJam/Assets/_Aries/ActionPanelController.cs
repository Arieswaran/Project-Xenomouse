using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActionPanelController : MonoBehaviour
{
    [SerializeField] private Button FeedButton, BrushButton, PlayButton, FeedCheeseButton , FeedPelletButton , RedCheeseButton, BlueCheeseButton, GreenCheeseButton,sendToMazeButton;

    [SerializeField] private List<GameObject> feedOptions, normalOptions;
    [SerializeField] private GameObject all_buttons_parent, cheese_buttons_parent;

    [SerializeField] private RaisePhaseController raisePhaseController;

    private void Start() {
        SetButtons();
    }

    private void SetButtons(){
        FeedButton.onClick.RemoveAllListeners();
        FeedButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(FeedButton.transform, ToggleFeedOptions);
        });
        BrushButton.onClick.RemoveAllListeners();
        BrushButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(BrushButton.transform, () => {
                raisePhaseController.TriggerBrushing();
            });
        });
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(PlayButton.transform, () => {
                raisePhaseController.TriggerPlaying();
            });
        });
        FeedCheeseButton.onClick.RemoveAllListeners();
        FeedCheeseButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(FeedCheeseButton.transform, () => {
                ToggleCheeseButtons();
                ToggleFeedOptions();               
            });
        });
        FeedPelletButton.onClick.RemoveAllListeners();
        FeedPelletButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(FeedPelletButton.transform, () => {
                raisePhaseController.TriggerEating();
                ToggleFeedOptions();
            });
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
            AnimationHelper.pressButton(RedCheeseButton.transform, () =>{
                raisePhaseController.TriggerEating();
                GameManager.instance.IncreaseMouseHealth(10);
                GameManager.instance.IncreaseRedCheese(-1);
                ToggleCheeseButtons();
                CheckCheeseAvailability();
            });
        });
        BlueCheeseButton.onClick.RemoveAllListeners();
        BlueCheeseButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(BlueCheeseButton.transform, () =>{
                raisePhaseController.TriggerEating();
                GameManager.instance.IncreaseMouseSpeed(20);
                GameManager.instance.IncreaseBlueCheese(-1);
                ToggleCheeseButtons();
                CheckCheeseAvailability();
            });
        });
        GreenCheeseButton.onClick.RemoveAllListeners();
        GreenCheeseButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(GreenCheeseButton.transform, () =>{
                raisePhaseController.TriggerEating();
                GameManager.instance.IncreaseMouseLifespan(20);
                GameManager.instance.IncreaseGreenCheese(-1);
                ToggleCheeseButtons();
                CheckCheeseAvailability();
            });
        });
        sendToMazeButton.onClick.RemoveAllListeners();
        sendToMazeButton.onClick.AddListener(() => {
            AnimationHelper.pressButton(sendToMazeButton.transform, () =>{
                GameManager.instance.LoadMazeScene();
            });
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
