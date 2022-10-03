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

    [SerializeField] private Queue<DialogueObject> availableDialogues;
    public Queue<DialogueObject> AvailableDialogues { get => availableDialogues; }

    [SerializeField] private Animator handlerAnimator;

    private DialogueHandler dialogueHandler;

    protected virtual void Start()
    {
        dialogueHandler = GetComponentInChildren<DialogueHandler>();

        relationshipWithPlayer = LoadNPCPoints();
        availableDialogues = new Queue<DialogueObject>(LoadAvailableDialogues());

        if (availableDialogues == null)
        {
            availableDialogues = new Queue<DialogueObject>();
        }

        if (availableDialogues?.Count > 0)
        {
            handlerAnimator.SetBool("hasDialogues", true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(Keybinds.KeyBinds[Actions.Use]) && CanStartDialogue() == true)
        {
            dialogueHandler.StartDialogue(availableDialogues.Peek());
        }
    }

    private void OnEnable()
    {
        DayNightCycle.HourPassedEvent += OnHourPassed;
    }

    private void OnDisable()
    {
        DayNightCycle.HourPassedEvent -= OnHourPassed;
    }

    public string GetDialoguesPath()
    {
        return "Dialogues/" + gameObject.name;
    }

    public void ChangeRelationshipWithPlayer(int value)
    {
        relationshipWithPlayer = Mathf.Clamp(relationshipWithPlayer + value, -100, 100);
        SaveNPCData();
    }

    public bool TryAddDialogue(DialogueObject dialogue)
    {
        if (IsDialogueValid(dialogue))
        {
            handlerAnimator.SetBool("hasDialogues", true);

            availableDialogues.Enqueue(dialogue);

            SaveAvailableDialoguesDataArray();

            return true;
        }

        return false;
    }

    public void RemoveFirstAvailableDialogue()
    {
        availableDialogues.Dequeue();

        if (availableDialogues.Count <= 0)
        {
            handlerAnimator.SetBool("hasDialogues", false);
        }

        SaveAvailableDialoguesDataArray();
    }

    protected abstract void OnHourPassed(TimeInfo timeInfo);

    private bool CanStartDialogue()
    {
        if (InteractAccessor.CanInteract && DialogueHandler.IsReading == false && dialogueHandler.InDialogueRange && availableDialogues.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    private void SaveNPCData()
    {
        SaveLoadSystem.SaveNPCData(this);
    }

    private int LoadNPCPoints()
    {
        NPCData data = SaveLoadSystem.LoadNPCData(this);

        return data.relationshipWithPlayer;
    }
}