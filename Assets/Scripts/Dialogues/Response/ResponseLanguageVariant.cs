using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResponseLanguageVariant
{
    [ReadOnly] public Languages language;

    [SerializeField] private string text;
    public string Text { get => text; }
}
