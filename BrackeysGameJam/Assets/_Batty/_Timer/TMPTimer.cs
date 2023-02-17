using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    

public class TMPTimer : MonoBehaviour
{


    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;
    [SerializeField] private Player player;

    public int Duration;
    public int remainingDuration;

    private void Start()
    {
        Duration = player.lifeSpan;
        Being(Duration);
    }

    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        
        while(remainingDuration >= 0)
        {

                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);

        }
        OnEnd();
    }

    private void OnEnd()
    {
        //End Time , if want Do something
        print("End");
    }
}
