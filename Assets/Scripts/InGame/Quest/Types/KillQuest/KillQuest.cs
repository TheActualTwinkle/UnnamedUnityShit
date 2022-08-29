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

    public void OnUnitDeath(object s, EventArgs e)
    {
        if (WhomToKill.GetType() == s.GetType())
        {
            AddCurrentKills(1);
        }
    }

    private void AddCurrentKills(int value)
    {
        currentKills += value;

        if (currentKills >= needKills)
            QuestCompleted?.Invoke(this, EventArgs.Empty);
    }

    [ContextMenu("ResetCurrentKills")]
    private void ResetCurrentKills()
    {
        currentKills = 0;
    }
}
