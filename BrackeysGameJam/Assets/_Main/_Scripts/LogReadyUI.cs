using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogReadyUI : MonoBehaviour
{
    [SerializeField] Image readyImage;

    private void Start()
    {
        if (GameManager.instance.GetGenerationCount() != 0) return;
        gameObject.SetActive(false);
    }


    public void SetLogChecked()
    {
        readyImage.gameObject.SetActive(false);
    }
}
