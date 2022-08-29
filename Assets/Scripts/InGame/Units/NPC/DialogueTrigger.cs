using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogueTrigger : Unit
{
    [ReadOnly]
    [SerializeField] private int relationshipWithPlayer;
    public int RelationshipWithPlayer { get => relationshipWithPlayer; }

    private List<DialogueObject> availableDialogues;
    public List<DialogueObject> AvailableDialogues { get => availableDialogues; }

    [SerializeField] private Animator handlerAnimator;

    private DayNightCycle dayNightCycle;
    private UnitDialogueHandler unitDialogueHandler;

    protected virtual void Start()
    {
        dayNightCycle = FindObjectOfType<DayNightCycle>();
        unitDialogueHandler = GetComponentInChildren<UnitDialogueHandler>();

        availableDialogues = new List<DialogueObject>();

        if (NewGameTuner.IsNewGame == false)
        {
            relationshipWithPlayer = LoadRelationshipPoints();
            availableDialogues = LoadAvailableDialogues().ToList();
        }

        if (availableDialogues.Count > 0)
        {
            handlerAnimator.SetBool("isEnable", true);
        }

        dayNightCycle.HourPassed += OnHourPassed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Keybinds.KeyBinds[Actions.Use]) && InteractAccessor.CanInteract)
        {
            TryTriggerDialogue();
        }
    }

    private void OnDestroy()
    {
        dayNightCycle.HourPassed -= OnHourPassed;
    }

    public string GetDialoguesPath()
    {
        return "Dialogues/" + gameObject.name;
    }

    public void ChangeRelationshipWithPlayer(int value)
    {
        relationshipWithPlayer = Mathf.Clamp(relationshipWithPlayer + value, -100, 100);

        SaveLoadSystem.SaveNPCData(this);
    }

    // Void `couse of button GUI (Fix on release)
    public void TryAddDialogue(DialogueObject dialogue)
    {
        if (IsDialogueValid(dialogue))
        {
            handlerAnimator.SetBool("isEnable", true);

            availableDialogues.Add(dialogue);

            SaveAvailableDialoguesDataArray();

            //return true;
        }

        //return false;
    }

    public void RemoveFirstAvailableDialogue()
    {
        RemoveAvailableDialogue(availableDialogues?.First());
    }

    protected abstract void OnHourPassed(object s, TimeInfo timeInfo);

    private void RemoveAvailableDialogue(DialogueObject dialogueObject)
    {
        availableDialogues.Remove(dialogueObject);

        if (availableDialogues.Count <= 0)
        {
            handlerAnimator.SetBool("isEnable", false);
        }

        RemoveUnvalidDialogues();

        SaveAvailableDialoguesDataArray();
    }

    private void RemoveUnvalidDialogues()
    {
        var alblDialogues = availableDialogues.ToArray();
        foreach (var dialogue in alblDialogues)
        {
            if (IsDialogueValid(dialogue) == false)
                RemoveAvailableDialogue(dialogue);
        }
    }

    private bool TryTriggerDialogue()
    {
        if (UnitDialogueHandler.IsReading == false && unitDialogueHandler.InDialogueRange && availableDialogues.Count > 0)
        {
            unitDialogueHandler.StartDialogue(availableDialogues[0]);
            return true;
        }
        return false;
    }

    private bool IsDialogueValid(DialogueObject dialogue)
    {
        if (dialogue.SpeakerName != UnitName)
        {
            Debug.LogWarning("You trying to add a wrong dialogue to " + UnitName + ". " + dialogue.SpeakerName + " is correct npc");
            return false;
        }

        if (relationshipWithPlayer < dialogue?.RequiredRelationshipPoints)
        {
            Debug.LogWarning("You dont have enough relationship points to use: " + dialogue.SpeakerName + "/" + dialogue.DialogueID);
            return false;
        }

        if (string.IsNullOrEmpty(dialogue.RequiredFact) == false && SaveLoadSystem.LoadFacts().Contains(dialogue.RequiredFact) == false)
        {
            Debug.LogWarning("You dont have fact " + dialogue.RequiredFact + " to use: " + dialogue.SpeakerName + "/" + dialogue.DialogueID);
            return false;
        }

        return true;
    }

    private void SaveAvailableDialoguesDataArray()
    {
        List<string> dialogueIDs = new List<string>();
        foreach (var availableDialogue in availableDialogues)
        {
            dialogueIDs.Add(availableDialogue.DialogueID);
        }

        SaveLoadSystem.SaveNPCDialogueData(UnitName, dialogueIDs.ToArray());
    }

    private DialogueObject[] LoadAvailableDialogues()
    {
        NPCDialoguesData data = SaveLoadSystem.LoadNPCDialogueData(UnitName);

        List<DialogueObject> dialogues = new List<DialogueObject>();

        if (data.NPCDialogueIDs != null)
        {
            foreach (var dialogueID in data.NPCDialogueIDs)
            {
                dialogues.Add(Resources.Load<DialogueObject>(GetDialoguesPath() + "/" + dialogueID));
            }
        }

        return dialogues.ToArray();
    }

    private int LoadRelationshipPoints()
    {
        NPCData d = SaveLoadSystem.LoadNPCData(this);

        return d.relationshipWithPlayer;
    }
}
