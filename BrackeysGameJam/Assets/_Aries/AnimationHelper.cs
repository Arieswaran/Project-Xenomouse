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

    public static void popupAnimation(Transform popup_content, GameObject overlay = null)
    {
        return; //not working as expected, need to fix
        if (popup_content == null)
        {
            return;
        }
        RectTransform rect = popup_content.GetComponent<RectTransform>();
        if (rect != null)
        {
            CanvasGroup canvas_group = popup_content.GetComponent<CanvasGroup>();
            if (canvas_group == null)
            {
                canvas_group = popup_content.gameObject.AddComponent<CanvasGroup>();
            }
            canvas_group.alpha = 0;
            canvas_group.DOFade(1, 0.3f);

            Vector3 original_scale = rect.localScale;
            rect.localScale = new Vector3(0, 0, 0);
            overlay?.gameObject.SetActive(true);
            rect.DOScale(new Vector3(original_scale.x + 0.07f, original_scale.y + 0.07f, original_scale.z), 0.2f).OnComplete(delegate ()
            {
                rect.DOScale(original_scale, 0.1f).OnComplete(delegate ()
                {
                    overlay?.gameObject.SetActive(false);
                });
            });
        }
    }

    public static void popupCloseAnimation(Transform popup_content, GameObject overlay = null, System.Action callback = null)
    {
        callback?.Invoke();
        return; //not working as expected, need to fix
        if (popup_content != null)
        {
            overlay?.gameObject.SetActive(true);
            RectTransform rect = popup_content.GetComponent<RectTransform>();
            if (rect != null)
            {
                CanvasGroup canvas_group = popup_content.GetComponent<CanvasGroup>();
                if (canvas_group == null)
                {
                    canvas_group = popup_content.gameObject.AddComponent<CanvasGroup>();
                }
                canvas_group.alpha = 1;
                canvas_group.DOFade(0, 0.3f);

                Vector3 original_scale = rect.localScale;
                rect.DOScale(new Vector3(original_scale.x - 0.2f, original_scale.y - 0.2f, original_scale.z), 0.3f).OnComplete(delegate ()
                {
                    rect.localScale = original_scale;
                    overlay?.gameObject.SetActive(false);
                    canvas_group.alpha = 1;
                    if (callback != null)
                    {
                        callback.Invoke();
                    }
                });
            }
        }
    }
}
