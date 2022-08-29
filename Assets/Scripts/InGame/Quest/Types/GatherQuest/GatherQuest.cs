using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GatherQuest", menuName = "QuestObject/GatherQuest")]
public class GatherQuest : QuestObject
{
    [Header("GatherQuest-------------------------------------------------------------------------")]

    [SerializeField] private GameObject whatToGather;
    public GameObject WhatToGather { get => whatToGather; }

    [SerializeField] private int needToGather;
    public int NeedToGather { get => needToGather; }

    [ReadOnly]
    [SerializeField] private int currentGather;
    public int CurrentGather { get => currentGather; }

}
