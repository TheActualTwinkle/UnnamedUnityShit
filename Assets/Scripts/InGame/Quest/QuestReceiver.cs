using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class QuestReceiver : MonoBehaviour
{
    [ReadOnly]
    [SerializeField] private QuestHandler questHandler;

    private QuestNotifier UINotifier;

    [ReadOnly]
    [SerializeField] private List<QuestObject> quests;
    public List<QuestObject> Quests { get => quests; }

    private void Start()
    {
        UINotifier = FindObjectOfType<QuestNotifier>();

        QuestReceiverData data = SaveLoadSystem.LoadQuestReceiverData();
        if (data != null)
        {
            foreach (var questID in data.questIDs)
            {
                QuestObject quest = Resources.Load<QuestObject>("Quests/ScriptableObjects/" + questID);
                AddQuest(quest, false);
            }
        }
    }

    public void AddQuest(QuestObject quest, bool notify = true)
    {
        quests.Add(quest);

        questHandler.StartHandleQuest(quest);

        if (notify == true)
        {
            UINotifier.NotifyAboutNewQuest(quest);
        }

        SaveLoadSystem.SaveQuestReceiverData(this);
    }

    public void RemoveQuest(QuestObject quest)
    {
        quests.Remove(quest);
        UINotifier.NotifyAboutEndQuest(quest);

        SaveLoadSystem.SaveQuestReceiverData(this);
    }
}
