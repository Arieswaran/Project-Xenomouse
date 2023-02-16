using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelController : MonoBehaviour
{
    [SerializeField] private Button healthButton, speedButton, lifespanButton;

    private void Awake()
    {
        SetButtons();
    }

    private void SetButtons(){
        healthButton.onClick.AddListener(delegate () {
                GameManager.instance.IncreaseMouseHealthByPercentageFromPreviousMouse(20);
                gameObject.SetActive(false);
            }
        );
        speedButton.onClick.AddListener(delegate () {
                GameManager.instance.IncreaseMouseSpeedByPercentageFromPreviousMouse(10);
                gameObject.SetActive(false);
            }
        );
        lifespanButton.onClick.AddListener(delegate () {
                GameManager.instance.IncreaseMouseLifespanByPercentageFromPreviousMouse(30);
                gameObject.SetActive(false);
            }
        );
    }
}
