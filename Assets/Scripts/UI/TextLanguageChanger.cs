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

    private void UpdateUI()
    {
        List<TextMeshProUGUI> texts = FindObjectsOfType<TextMeshProUGUI>(true).ToList();
        List<Text> notTextMeshProTexts = FindObjectsOfType<Text>(true).ToList();

        Dictionary<string, List<string>> UIText = CSVReader.GetFileInfo(UITextFile);

        foreach (var text in texts)
        {
            if (text.name.Contains("ignoreCSV"))
            {
                continue;
            }

            if (UIText.TryGetValue(text.name, out List<string> translations))
            {
                int indexForTranslations = (int)Language.GetCurrentLanguage() - 1;
                text.text = translations[indexForTranslations];
            }
            else
            {
                Debug.LogError("Missing translation for '" + text.name + "' in " + UITextFile.name);
            }
        }

        foreach (var t in notTextMeshProTexts)
        {
            Debug.LogWarning("There is no TextMeshProUGUI component at " + t.name);
        }
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
}
