using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public const int MAX_RESPONSE_OPTIONS = 4;

    private DialogueDisplayer dialogueDisplayer;
    private ResponseOption currentResponse;
    private TextMeshProUGUI text;

    public void Setup(ResponseOption responseOption)
    { 
        text = GetComponentInChildren<TextMeshProUGUI>();
        dialogueDisplayer = GetComponentInParent<DialogueDisplayer>();

        text.text = responseOption.LanguageVariants[(int)Language.GetCurrentLanguage() - 1].Text;
        currentResponse = responseOption;

        List<string> savedFacts = SaveLoadSystem.LoadFacts()?.ToList();
        if (savedFacts.Contains(currentResponse.RequiredFactID) || currentResponse.RequiredFactID == "")
        {
            gameObject.SetActive(true);
        }
    }

    public void Disable()
    {
        currentResponse = null;

        GetComponentInChildren<TextMeshProUGUI>().text = null;
        gameObject.SetActive(false);
    }

    //Button.
    private void ExecuteEffect()
    {
        DialogueTrigger dialogueTrigger = UnitDialogueHandler.CurrentSpeaker;
        dialogueTrigger.ChangeRelationshipWithPlayer(currentResponse.RelationshipsEffect);

        if (currentResponse?.QuestObject != null)
        {
            var questReceiver = FindObjectOfType<QuestReceiver>();
            questReceiver.AddQuest(currentResponse.QuestObject);
        }

        if (currentResponse?.AddableFactID != "")
        {
            SaveLoadSystem.SaveFact(currentResponse.AddableFactID);
        }

        if (currentResponse?.DialogueObject != null)
        {
            dialogueDisplayer.DisplayDialogue(currentResponse.DialogueObject);
            return;
        }

        DialogueDisplayer.DialogueDisplayed?.Invoke(this, EventArgs.Empty);
    }
}
