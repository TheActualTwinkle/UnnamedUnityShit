using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestLanguageVariant
{
    [ReadOnly] public Languages language;

    [SerializeField] private string questName;
    public string QuestName { get => questName; }

    [SerializeField] private string location;
    public string Location { get => location; }

    [SerializeField] private List<string> whatToDo;
    public List<string> WhatToDo { get => whatToDo; }
}
