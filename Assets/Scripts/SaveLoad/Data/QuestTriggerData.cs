using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestTriggerData
{
    public readonly string questID;

    public QuestTriggerData(QuestTrigger questTrigger)
    {
        questID = questTrigger?.QuestToActivate?.QuestID;
    }
}
