using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLanguageVariant
{
    [ReadOnly] public Languages language;

    [TextArea(3, 3)]
    [SerializeField] private List<string> paragraphs;
    public List<string> Paragraphs { get => paragraphs; }
}
