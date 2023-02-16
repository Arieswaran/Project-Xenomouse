using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

public static class AnimationHelper 
{
    public static void pressButton(Transform btn, System.Action call_back = null)
    {
        float positionOffset = 0.3f, timeDuration = 0.1f;
        Button button_object = btn.GetComponent<Button>();
        if (button_object != null)
        {
            button_object.interactable = false;
        }
        var btnRt = btn.GetComponent<RectTransform>();
        Vector3 orgScale = btnRt.localScale;
        btnRt.DOScale(new Vector3(orgScale.x - positionOffset, orgScale.y - positionOffset, orgScale.z), timeDuration).OnComplete(delegate
        {
            btnRt.DOScale(orgScale, timeDuration).OnComplete(delegate
            {
                call_back?.Invoke();
                if (button_object != null)
                {
                    button_object.interactable = true;
                }
            });
        });
    }

    public static void DoNumberAnimation(TextMeshProUGUI text, int start, int end, float duration = 2f, System.Action call_back = null)
    {
        text.text = start.ToString();
        DOTween.To(() => start, x => text.text = x.ToString(), end, duration).OnComplete(delegate
        {
            call_back?.Invoke();
        });
    }

    public static void DoNumberJumpAnimation(TextMeshProUGUI text, int start, int end, float duration = 2f, System.Action call_back = null)
    {
        text.text = start.ToString();
        DOTween.To(() => start, x => text.text = x.ToString(), end, duration).SetEase(Ease.OutBounce).OnComplete(delegate
        {
            call_back?.Invoke();
        });
    }
}
