using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class QuestReceiver : MonoBehaviour
{
    public event Action<QuestObject> QuestStartEvent;
    public event Action<QuestObject> QuestEndEvent;

    [ReadOnly]
    [SerializeField] private List<QuestObject> quests;
    public List<QuestObject> Quests => quests;

    private void Start()
    {
        QuestReceiverData data = SaveLoadSystem.LoadQuestReceiverData();
        if (data != null)
        {
            foreach (var questID in data.questIDs)
            {
                QuestObject quest = Resources.Load<QuestObject>("Quests/ScriptableObjects/" + questID);
                AddQuest(quest);
            }
        }
    }

    public void AddQuest(QuestObject quest)
    {
        quests.Add(quest);

        QuestStartEvent?.Invoke(quest);

        SaveLoadSystem.SaveQuestReceiverData(this);
    }

    public void EndQuest(QuestObject quest)
    {
        quests.Remove(quest);

        QuestEndEvent?.Invoke(quest);

        SaveLoadSystem.SaveQuestReceiverData(this);
    }
}
