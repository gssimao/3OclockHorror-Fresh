﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        if (slider != null)
        {
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        }
    }
    public void SetLevel(float sliderValue)
    {
        sliderValue = slider.value;
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
