using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private QuestObject questToActivate;
    public QuestObject QuestToActivate => questToActivate;

    private void Start()
    {
        if (NewGameTuner.IsNewGame == false)
        {
            questToActivate = Resources.Load<QuestObject>("Quests/Objects" + SaveLoadSystem.LoadQuestTriggerData(this).questID);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out QuestReceiver questReceiver))
        {
            if (questToActivate != null)
            {
                questReceiver.AddQuest(questToActivate);
                questToActivate = null;

                SaveLoadSystem.SaveQuestTriggerData(this);
            }
        }
    }
}
