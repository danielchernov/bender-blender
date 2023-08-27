using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider soundSlider;

    void Start()
    {
        if (PlayerPrefs.GetFloat("volume") != 0)
        {
            soundSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            soundSlider.value = 0.5f;
        }
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }
}
