using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class TextLanguageChanger : MonoBehaviour
{
    [SerializeField] private TextAsset UITextFile;

    private void Start()
    {
        UpdateUI();
    }

    // Button
    private void ChangeTextLanguage(string language)
    {
        if (language != Language.GetCurrentLanguage().ToString())
        {
            Language.SetNewLanguage((Languages)Enum.Parse(typeof(Languages), language));

            UpdateUI();
        }
        SaveLoadSystem.SaveGameData();
    }

    private void UpdateUI()
    {
        List<TextMeshProUGUI> texts = GetComponentsInChildren<TextMeshProUGUI>(true).ToList();
        List<Text> notTextMeshProTexts = GetComponentsInChildren<Text>(true).ToList();

        Dictionary<string, List<string>> UIText = CSVReader.GetFileInfo(UITextFile);

        //for (int i = 1 + Enum.GetNames(typeof(Languages)).Length; i < UIText.Length; i++)
        //{
        //    foreach (var text in texts)
        //    {
        //        if (text.name == UIText[i])
        //        {
        //            text.text = UIText[i + (int)Language.GetCurrentLanguage()].Remove(0, 1);
        //        }
        //    }
        //}

        foreach (var t in notTextMeshProTexts)
        {
            Debug.LogWarning("Объект '" + t.name + "' не содержит компонент TextMeshProUGUI\nСмена языков работает не корректно");
        }
    }
}
