using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillQuest", menuName = "QuestObject/KillQuest")]
public class KillQuest : QuestObject
{
    [Header("KillQuest-------------------------------------------------------------------------")]

    [SerializeField] private Unit whomToKill;
    public Unit WhomToKill { get => whomToKill; }

    [SerializeField] private int needKills;
    public int NeedKills { get => needKills; }

    [ReadOnly]
    [SerializeField] private int currentKills;
    public int CurrentKills { get => currentKills; }

    public void OnUnitDeath(Unit unit)
    {
        if (WhomToKill.GetType() == unit.GetType())
        {
            currentKills++;
            if (currentKills >= needKills)
            {
                CallQuestCompletedEvent();
            }
        }
    }

    [ContextMenu("ResetCurrentKills")]
    private void ResetCurrentKills()
    {
        currentKills = 0;
    }
}
