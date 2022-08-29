using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetVolume : MonoBehaviour
{
    public static float masterSliderValue = 1f;
    public static float sfxSliderValue = 1f;
    public static float musicSliderValue = 1f;

    public AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    public void Init()
    {
        if (File.Exists(SaveLoadSystem.gameSaveFile) == false)
            SetDefaultValue();
        else
            SetSavedVolume();
    }

    // Устанавливаю общюю громкость в миксере на значение взятое с слайдера 
    private void SetMasterVolumeLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        masterSliderValue = sliderValue;
        masterSlider.value = masterSliderValue;
    }

    // Устанавливаю громкость эффектов в миксере на значение взятое с слайдера 
    private void SetSFXVolumeLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        sfxSliderValue = sliderValue;
        sfxSlider.value = sfxSliderValue;
    }

    // Устанавливаю громкость музыки в миксере на значение взятое с слайдера 
    private void SetMusicVolumeLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        musicSliderValue = sliderValue;
        musicSlider.value = musicSliderValue;
    }

    // For OnSliderChange
    private void SaveVolume()
    {
        SaveLoadSystem.SaveGameData();
    }

    private void SetSavedVolume()
    {
        GameData data = SaveLoadSystem.LoadGameData();
        SetMasterVolumeLevel(data.masterSliderValue);
        SetSFXVolumeLevel(data.sfxSliderValue);
        SetMusicVolumeLevel(data.musicSliderValue);
    }

    private void SetDefaultValue()
    {
        SetMasterVolumeLevel(1f);
        SetSFXVolumeLevel(1f);
        SetMusicVolumeLevel(1f);
    }
}
