using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[System.Serializable]
public class Language
{
    private static Languages currentLanguage;

    public static Languages GetCurrentLanguage()
    {
        if (currentLanguage == 0) // If unassigned.
        {
            int bracketIndex = CultureInfo.CurrentCulture.EnglishName.IndexOf(' ');
            string systemLanguage = CultureInfo.CurrentCulture.EnglishName.Remove(bracketIndex);
            if (Enum.TryParse(systemLanguage, out Languages language) == true)
            {
                currentLanguage = language;
            }
            else
            {
                currentLanguage = Languages.English;
            }
        }

        return currentLanguage;
    }

    public static void SetNewLanguage(Languages language)
    {
        currentLanguage = language;
    }
}

// Do not delete.
public enum Languages
{
    English = 1,
    Russian,
}