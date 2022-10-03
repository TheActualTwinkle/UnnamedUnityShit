using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(QuestReceiver))]
public class QuestHandler : MonoBehaviour
{
    [ReadOnly]
    [SerializeField] private QuestReceiver questReceiver;

    private List<EscortQuest> escortQuests = new List<EscortQuest>();
    private List<GatherQuest> gatherQuests = new List<GatherQuest>();
    private List<KillQuest> killQuests = new List<KillQuest>();
    private List<DeliveryQuest> deliveryQuests = new List<DeliveryQuest>();

    private void OnEnable()
    {
        questReceiver.QuestStartEvent += StartHandleQuest;
    }

    private void OnDisable()
    {
        questReceiver.QuestStartEvent -= StartHandleQuest;
    }

    // GENERAL -------------------------------------------

    public void StartHandleQuest(QuestObject quest)
    {
        switch (quest.QuestType)
        {
            case QuestType.Escort:
                StartHandleQuest((EscortQuest)quest);
                break;

            case QuestType.Gather:
                StartHandleQuest((GatherQuest)quest);
                break;

            case QuestType.Kill:
                StartHandleQuest((KillQuest)quest);
                break;

            case QuestType.Delivery:
                StartHandleQuest((DeliveryQuest)quest);
                break;
        }

        quest.QuestCompletedEvent += OnQuestCompleted;
    }

    private void StopHandleQuest(QuestObject quest)
    {
        switch (quest.QuestType)
        {
            case QuestType.Escort:
                StopHandleQuest((EscortQuest)quest);
                break;

            case QuestType.Gather:
                StopHandleQuest((GatherQuest)quest);
                break;

            case QuestType.Kill:
                StopHandleQuest((KillQuest)quest);
                break;

            case QuestType.Delivery:
                StopHandleQuest((DeliveryQuest)quest);
                break;
        }

        quest.QuestCompletedEvent -= OnQuestCompleted;
    }

    // START ---------------------------------------------

    private void StartHandleQuest(EscortQuest quest)
    {
        escortQuests.Add(quest);
        EscortZone.EscortedEvent += quest.OnEscorted;
    }

    private void StartHandleQuest(GatherQuest quest)
    {
        gatherQuests.Add(quest);
    }

    private void StartHandleQuest(KillQuest quest)
    {
        killQuests.Add(quest);
        Unit.DeathEvent += quest.OnUnitDeath;
    }

    private void StartHandleQuest(DeliveryQuest quest)
    {
        deliveryQuests.Add(quest);
    }

    // STOP ----------------------------------------------

    private void StopHandleQuest(EscortQuest quest)
    {
        escortQuests.Remove(quest);
        EscortZone.EscortedEvent -= quest.OnEscorted;
    }

    private void StopHandleQuest(GatherQuest quest)
    {
        gatherQuests.Remove(quest);
    }

    private void StopHandleQuest(KillQuest quest)
    {
        killQuests.Remove(quest);
        Unit.DeathEvent -= quest.OnUnitDeath;
    }

    private void StopHandleQuest(DeliveryQuest quest)
    {
        deliveryQuests.Remove(quest);
    }

    // OTEHR ---------------------------------------------

    private void OnQuestCompleted(QuestObject quest)
    {
        StopHandleQuest(quest);
        questReceiver.EndQuest(quest);
    }
}
