using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    void Start()
    {
    }
    public void SetLevel()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("volume", Mathf.Log10(sliderValue) * 20);
    }
}