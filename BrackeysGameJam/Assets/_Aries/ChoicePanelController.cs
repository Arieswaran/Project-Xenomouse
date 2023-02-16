using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelController : MonoBehaviour
{
    [SerializeField] private Button healthButton, speedButton, lifespanButton;
    [SerializeField] private GameObject mouse;

    private void Awake()
    {
        SetButtons();
    }

    private void SetButtons(){
        healthButton.onClick.AddListener(delegate () {
                AnimationHelper.pressButton(healthButton.transform,delegate(){
                    GameManager.instance.IncreaseMouseHealthByPercentageFromPreviousMouse(20);
                    gameObject.SetActive(false);
                });
            }
        );
        speedButton.onClick.AddListener(delegate () {
                AnimationHelper.pressButton(speedButton.transform,delegate(){
                    GameManager.instance.IncreaseMouseSpeedByPercentageFromPreviousMouse(10);
                    gameObject.SetActive(false);
                });
            }
        );
        lifespanButton.onClick.AddListener(delegate () {
                AnimationHelper.pressButton(lifespanButton.transform,delegate(){
                    GameManager.instance.IncreaseMouseLifespanByPercentageFromPreviousMouse(30);
                    gameObject.SetActive(false);
                });
            }
        );
    }

    private void OnEnable() {
        mouse.SetActive(false);
    }

    private void OnDisable() {
        mouse.SetActive(true);
        RaisePhaseController.instance.TriggerRevive();
    }
}
