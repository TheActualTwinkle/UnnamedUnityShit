using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class QuestReceiverData
{
    public readonly List<string> questIDs;

    public QuestReceiverData(QuestReceiver questReceiver)
    {
        questIDs = questReceiver?.Quests?.Select(x => x.QuestID).ToList();
    }
}
