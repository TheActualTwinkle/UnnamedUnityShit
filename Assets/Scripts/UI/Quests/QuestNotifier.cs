using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class QuestNotifier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI newQuestText;
    [SerializeField] private TextMeshProUGUI endQuestText;
    [SerializeField] private TextMeshProUGUI questNameText;

    private Queue<QuestObject> unnotifiedNewQuests = new Queue<QuestObject>();
    private Queue<QuestObject> unnotifiedEndQuests = new Queue<QuestObject>();

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void NotifyAboutNewQuest(QuestObject quest)
    {
        if (animator.GetCurrentAnimatorClipInfo(0).First().clip.name == "Idle")
        {
            SetupNotifier(quest);
            animator.SetTrigger("NewQuest");
        }
        else
        {
            unnotifiedNewQuests.Enqueue(quest);
        }
    }

    public void NotifyAboutEndQuest(QuestObject quest)
    {
        if (animator.GetCurrentAnimatorClipInfo(0).First().clip.name == "Idle")
        {
            SetupNotifier(quest);
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
        else if (unnotifiedNewQuests.Count > 0)
        {
            NotifyAboutNewQuest(unnotifiedNewQuests.Peek());
            unnotifiedNewQuests.Dequeue();
        }
    }

    private void SetupNotifier(QuestObject quest)
    {
        questNameText.text = quest.LanguageVariants.Where(x => x.language == Language.GetCurrentLanguage()).First().QuestName;
    }
}
