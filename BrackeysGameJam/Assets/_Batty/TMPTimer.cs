using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TMPTimer : MonoBehaviour
{


    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    public int Duration;

    private int remainingDuration;

    private bool Pause;

    private void Start()
    {
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
