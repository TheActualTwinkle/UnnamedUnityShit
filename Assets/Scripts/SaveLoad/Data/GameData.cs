using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class GameData
{
    public readonly Languages savedLanguage;

    public readonly float masterSliderValue;
    public readonly float sfxSliderValue;
    public readonly float musicSliderValue;

    public GameData()
    {
        savedLanguage = Language.GetCurrentLanguage();

        masterSliderValue = SetVolume.masterSliderValue;
        sfxSliderValue = SetVolume.sfxSliderValue;
        musicSliderValue = SetVolume.musicSliderValue;
    }
}
