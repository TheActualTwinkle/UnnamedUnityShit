using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class CSVReader
{
    public static Dictionary<string, List<string>> GetFileInfo(TextAsset textAssetData)
    {
        List<string> rawCSVData = textAssetData.text.Split(new string[] { ";", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        Dictionary<string, List<string>> translations = new Dictionary<string, List<string>>();
        int lineStep = 1 + Enum.GetNames(typeof(Languages)).Length;

        if (IsFileSupportAllLanguages(rawCSVData, out List<string> notSupportedLanguages) == false)
        {
            Debug.LogError("File doesn`t support some languages: " + string.Join(", ", notSupportedLanguages.ToArray()));
            return null;
        }

        if (IsLanguagesSorted(rawCSVData, lineStep) == false)
        {
            Debug.LogError("File doesn`t sorted by 'Languages' enum. Must be: " + string.Join(", ", Enum.GetNames(typeof(Languages))));
            return null;
        }

        string textID;
        List<string> translation = new List<string>();
        for (int i = lineStep; i < rawCSVData.Count; i += lineStep)
        {
            translation.Clear();
            textID = rawCSVData[i];
            if (textID.EndsWith("Text") == false)
            {
                Debug.LogError("Fatal: translation is broken. Somewhere on: )" + textID);
                continue;
            }

            for (int j = i+1; j < i + lineStep; j++)
            {
                translation.Add(rawCSVData[j]);
            }

            translations.Add(textID, translation.ToList());
        }

        return translations;
    }

    private static bool IsFileSupportAllLanguages(List<string> rawCSVData, out List<string> notSupportedLanguages)
    {
        int lineStep = 1 + Enum.GetNames(typeof(Languages)).Length;

        List<string> supportedLanguages = Enum.GetNames(typeof(Languages)).ToList();
        List<string> presentedLanguages = GetPresentedLanguages(rawCSVData, lineStep);
        notSupportedLanguages = GetNotSupportedLanguages(supportedLanguages, presentedLanguages);

        if (supportedLanguages.Count == presentedLanguages.Count - notSupportedLanguages.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static bool IsLanguagesSorted(List<string> rawCSVData, int lineStep)
    {
        string[] supportedLanguages = Enum.GetNames(typeof(Languages));
        List<string> presentedLanguages = GetPresentedLanguages(rawCSVData, lineStep);

        for (int i = 0; i < supportedLanguages.Length; i++)
        {
            if (supportedLanguages[i] != presentedLanguages[i])
            {
                return false;
            }
        }

        return true;
    }

    private static List<string> GetPresentedLanguages(List<string> rawCSVData, int lineStep)
    {
        List<string> presentedLanguages = new List<string>();

        for (int i = 1; i < lineStep; i++)
        {
            presentedLanguages.Add(rawCSVData[i]);
        }

        return presentedLanguages;
    }

    private static List<string> GetNotSupportedLanguages(List<string> supportedLanguages, List<string> presentedLanguages)
    {
        List<string> notSupportedLanguages = new List<string>();
        for (int i = 0; i < supportedLanguages.Count; i++)
        {
            if (presentedLanguages.Contains(supportedLanguages[i]) == false)
            {
                notSupportedLanguages.Add(supportedLanguages[i]);
            }
        }

        return notSupportedLanguages;
    }
}