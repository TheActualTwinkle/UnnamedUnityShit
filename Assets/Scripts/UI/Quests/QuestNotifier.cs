using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class QuestNotifier : MonoBehaviour
{
    [SerializeField] private QuestReceiver questReceiver;

    [SerializeField] private TextMeshProUGUI newQuestText;
    [SerializeField] private TextMeshProUGUI endQuestText;
    [SerializeField] private TextMeshProUGUI questNameText;

    private Queue<QuestObject> unnotifiedStartQuests = new Queue<QuestObject>();
    private Queue<QuestObject> unnotifiedEndQuests = new Queue<QuestObject>();

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        questReceiver.QuestStartEvent += NotifyAboutStartQuest;
        questReceiver.QuestEndEvent += NotifyAboutEndQuest;
    }

    private void OnDisable()
    {
        questReceiver.QuestStartEvent -= NotifyAboutStartQuest;
        questReceiver.QuestEndEvent -= NotifyAboutEndQuest;
    }

    public void NotifyAboutStartQuest(QuestObject quest)
    {
        if (animator.GetCurrentAnimatorClipInfo(0).First().clip.name == "Idle")
        {
            SetQuestNameText(quest);
            animator.SetTrigger("StartQuest");
        }
        else
        {
            unnotifiedStartQuests.Enqueue(quest);
        }
    }

    public void NotifyAboutEndQuest(QuestObject quest)
    {
        if (animator.GetCurrentAnimatorClipInfo(0).First().clip.name == "Idle")
        {
            SetQuestNameText(quest);
            animator.SetTrigger("EndQuest");
        }
        else
        {
            unnotifiedEndQuests.Enqueue(quest);
        }
    }

    // Animator.
    private void OnEndOfAnimation()
    {
        if (unnotifiedEndQuests.Count > 0)
        {
            NotifyAboutEndQuest(unnotifiedEndQuests.Peek());
            unnotifiedEndQuests.Dequeue();
        }        
        else if (unnotifiedStartQuests.Count > 0)
        {
            NotifyAboutStartQuest(unnotifiedStartQuests.Peek());
            unnotifiedStartQuests.Dequeue();
        }
    }

    private void SetQuestNameText(QuestObject quest)
    {
        questNameText.text = quest.LanguageVariants.Where(x => x.language == Language.GetCurrentLanguage()).First().QuestName;
    }
}
