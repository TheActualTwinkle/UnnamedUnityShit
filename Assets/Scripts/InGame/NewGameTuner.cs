using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NewGameTuner : MonoBehaviour
{
    public static bool IsNewGame { get { return !IsPlayerSaveExist(); } }
    public static bool IsFirstExecute { get { return !IsGameSaveExist(); } }

    [SerializeField] private List<DialogueObject> firstDialogues;

    private void Awake()
    {
        if (IsFirstExecute == true)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");

            Directory.CreateDirectory(Application.persistentDataPath + "/saves/NPC");
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/WorldEventTriggers");
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/QuestTriggers");
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/Mirror");
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/Facts");
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/QuestReceiver");
        }
        else
        {
            var playerData = SaveLoadSystem.LoadPlayerData();
            if (playerData != null)
            {
                RespawnPoint.SetRespawnPosition(playerData.respawnPos);
            }
        }
    }

    private void Start()
    {
        if (IsNewGame == true)
        {
            StartCoroutine(SetupFirstDialogues());
        }
    }

    private static bool IsPlayerSaveExist()
    {
        return File.Exists(SaveLoadSystem.playerSaveFile);
    }

    private static bool IsGameSaveExist()
    {
        return File.Exists(SaveLoadSystem.gameSaveFile);
    }

    private IEnumerator SetupFirstDialogues()
    {
        yield return new WaitForEndOfFrame();

        Dictionary<string, List<DialogueObject>> dialoguePairs = new Dictionary<string, List<DialogueObject>>();
        foreach (var dialogue in firstDialogues)
        {
            dialoguePairs.Add(dialogue.SpeakerName, firstDialogues.Where(d => d.SpeakerName == dialogue.SpeakerName).ToList());
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
}