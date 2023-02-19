using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    /// <summary>
    /// from Batty's script at https://github.com/Arieswaran/Brackeys-GameJam-2023-1/blob/Batty%27s-Branch/BrackeysGameJam/Assets/VolumeSlider.cs
    /// </summary>
    [SerializeField] AudioMixer mixer;
    Slider slider;

    const string AUDIOMIXER_MASTER_VOLUME = "MasterVolume";

    void Start()
    {
        slider = GetComponent<Slider>();


        slider.onValueChanged.AddListener((float volume) =>
        {
            SetLevel(slider.value);
        });

        

        SetLevel(slider.value);

    }
    void SetLevel(float sliderValue)
    {
        mixer.SetFloat(AUDIOMIXER_MASTER_VOLUME, Mathf.Log10(sliderValue) * 20);
    }

}
