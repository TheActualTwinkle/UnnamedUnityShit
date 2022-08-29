using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "WorldEvent", menuName = "WorldEvent")]
public class WorldEventObject : ScriptableObject
{
    [ReadOnly]
    [SerializeField] private string eventID;
    public string EventID { get => eventID; }

    [SerializeField] private bool executeOnce;
    public bool ExecuteOnce { get => executeOnce; }

    [SerializeField] private List<DialogueObject> addableDialogues;

    [SerializeField] private List<string> addableFacts;

    private void OnValidate()
    {
        SetID();
    }

    public void Execute(EventTrigger eventTrigger, ref WorldEventObject worldEvent)
    {
        if (worldEvent != null)
        {
            AddDialogues();
            AddFacts();

            if (worldEvent.executeOnce)
                worldEvent = null;

            SaveLoadSystem.SaveEventTriggerData(eventTrigger);
        }
    }

    private void AddDialogues()
    {
        Dictionary<string, List<DialogueObject>> dialoguePairs = new Dictionary<string, List<DialogueObject>>();
        foreach (var dialogue in addableDialogues)
        {
            dialoguePairs.Add(dialogue.SpeakerName, addableDialogues.Where(d => d.SpeakerName == dialogue.SpeakerName).ToList());
        }

        List<DialogueTrigger> activeNPCs = FindObjectsOfType<DialogueTrigger>().ToList();

        foreach (var npc in activeNPCs)
        {
            List<DialogueObject> dialoguesToAdd;
            if (dialoguePairs.TryGetValue(npc.UnitName, out dialoguesToAdd))
            {
                foreach (var dialogue in dialoguesToAdd)
                {
                    npc.TryAddDialogue(dialogue);
                }
                dialoguePairs.Remove(npc.UnitName);
            }
        }

        foreach (var pair in dialoguePairs)
        {
            List<string> NPCDialogueIDs = SaveLoadSystem.LoadNPCDialogueData(pair.Key)?.NPCDialogueIDs;
            if (NPCDialogueIDs == null)
                NPCDialogueIDs = new List<string>();

            List<DialogueObject> dialoguesToAdd;
            if (dialoguePairs.TryGetValue(pair.Key, out dialoguesToAdd))
            {
                List<string> dialogueIDs = dialoguesToAdd.Select(x => x.DialogueID).ToList();
                foreach (var dialogueID in dialogueIDs)
                {
                    NPCDialogueIDs.Add(dialogueID);
                }
            }

            SaveLoadSystem.SaveNPCDialogueData(pair.Key, NPCDialogueIDs.ToArray());
        }
    }

    private void AddFacts()
    {
        foreach (var fact in addableFacts)
        {
            SaveLoadSystem.SaveFact(fact);
        }
    }

    private void SetID()
    {
#if UNITY_EDITOR
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        string extension = Path.GetExtension(assetPath);

        string[] dialogueInfo = assetPath.Substring(assetPath.LastIndexOf("WorldEvents/") + "WorldEvents/".Length).Split('/');

        eventID = null;
        for (int i = 0; i < dialogueInfo.Length; i++)
        {
            eventID += dialogueInfo[i] + "/";
        }
        eventID = eventID.Substring(0, eventID.Length - extension.Length - 1);
#endif
    }
}
