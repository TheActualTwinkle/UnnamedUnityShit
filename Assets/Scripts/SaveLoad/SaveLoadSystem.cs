using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveLoadSystem
{
    public readonly static string gameSaveFile = Application.persistentDataPath + "/saves/gameData.df";
    public readonly static string playerSaveFile = Application.persistentDataPath + "/saves/playerData.df";
    public readonly static string mirrorDataSaveFile = Application.persistentDataPath + "/saves/Mirror/mirrorData.df";
    public readonly static string factDataSaveFile = Application.persistentDataPath + "/saves/Facts/factsData.df";    
    public readonly static string questReceiverDataSaveFile = Application.persistentDataPath + "/saves/QuestReceiver/questReceiverData.df";

    public readonly static string npcDataSavePath = Application.persistentDataPath + "/saves/NPC";
    public readonly static string eventTriggerDataSavePath = Application.persistentDataPath + "/saves/WorldEventTriggers";    
    public readonly static string questTriggerDataSavePath = Application.persistentDataPath + "/saves/QuestTriggers";
    public readonly static string escortZoneDataSavePath = Application.persistentDataPath + "/saves/EscortZone";

    public static EventHandler DataSavedEvent;

    private static BinaryFormatter bf = new BinaryFormatter();  

    public static void SaveGameData()
    {
        FileStream fileStream = new FileStream(gameSaveFile, FileMode.Create);

        GameData gameData = new GameData();

        bf.Serialize(fileStream, gameData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(gameSaveFile))
        {
            FileStream fileStream = new FileStream(gameSaveFile, FileMode.Open);

            GameData gameData = bf.Deserialize(fileStream) as GameData;

            fileStream.Close();

            return gameData;
        }
        else
        {
            return null;
        }
    }


    public static void SavePlayerData(PlayerController player)
    {
        FileStream fileStream = new FileStream(playerSaveFile, FileMode.Create);

        PlayerData playerData = new PlayerData(player);

        bf.Serialize(fileStream, playerData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(playerSaveFile))
        {
            FileStream fileStream = new FileStream(playerSaveFile, FileMode.Open);

            PlayerData playerData = bf.Deserialize(fileStream) as PlayerData;

            fileStream.Close();

            return playerData;
        }
        else
        {
            return null;
        }
    }


    public static void SaveNPCDialogueData(string npcName, string[] dialogueIDs)
    {
        FileStream fileStream = new FileStream(npcDataSavePath + "/" + "dlg_" + npcName + ".df", FileMode.Create);

        NPCDialoguesData npcDialoguesData = new NPCDialoguesData(dialogueIDs);

        bf.Serialize(fileStream, npcDialoguesData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static NPCDialoguesData LoadNPCDialogueData(string npcName)
    {
        string fullPath = npcDataSavePath + "/" + "dlg_" + npcName + ".df";

        if (File.Exists(fullPath))
        {
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);

            NPCDialoguesData npcDialoguesData = bf.Deserialize(fileStream) as NPCDialoguesData;

            fileStream.Close();

            return npcDialoguesData;
        }
        else
        {
            return new NPCDialoguesData(null);
        }
    }


    public static void SaveNPCData(DialogueTrigger npc)
    {
        FileStream fileStream = new FileStream(npcDataSavePath + "/" + "oth_" + npc.name + ".df", FileMode.Create);

        NPCData playerData = new NPCData(npc);

        bf.Serialize(fileStream, playerData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static NPCData LoadNPCData(DialogueTrigger npc)
    {
        string fullPath = npcDataSavePath + "/" + "oth_" + npc.name + ".df";

        if (File.Exists(fullPath))
        {
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);

            NPCData npcData = bf.Deserialize(fileStream) as NPCData;

            fileStream.Close();

            return npcData;
        }
        else
        {
            return new NPCData(npc);
        }
    }


    public static void SaveEventTriggerData(EventTrigger eventTrigger)
    {
        FileStream fileStream = new FileStream(eventTriggerDataSavePath + "/" + eventTrigger.name + ".df", FileMode.Create);

        EventTriggerData eventTriggerData = new EventTriggerData(eventTrigger);

        bf.Serialize(fileStream, eventTriggerData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static EventTriggerData LoadEventTriggerData(EventTrigger eventTrigger)
    {
        string fullPath = eventTriggerDataSavePath + "/" + eventTrigger.name + ".df";

        if (File.Exists(fullPath))
        {
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);

            EventTriggerData eventTriggerData = bf.Deserialize(fileStream) as EventTriggerData;

            fileStream.Close();

            return eventTriggerData;
        }
        else
        {
            return new EventTriggerData(eventTrigger);
        }
    }


    public static void SaveQuestReceiverData(QuestReceiver questReceiver)
    {
        FileStream fileStream = new FileStream(questReceiverDataSaveFile, FileMode.Create);

        QuestReceiverData questReceiverData = new QuestReceiverData(questReceiver);

        bf.Serialize(fileStream, questReceiverData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static QuestReceiverData LoadQuestReceiverData()
    {
        if (File.Exists(questReceiverDataSaveFile))
        {
            FileStream fileStream = new FileStream(questReceiverDataSaveFile, FileMode.Open);

            QuestReceiverData questReceiverData = bf.Deserialize(fileStream) as QuestReceiverData;

            fileStream.Close();

            return questReceiverData;
        }
        else
        {
            return null;
        }
    }


    public static void SaveQuestTriggerData(QuestTrigger questTrigger)
    {
        FileStream fileStream = new FileStream(questTriggerDataSavePath + "/" + questTrigger.name + ".df", FileMode.Create);

        QuestTriggerData questTriggerData = new QuestTriggerData(questTrigger);

        bf.Serialize(fileStream, questTriggerData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static QuestTriggerData LoadQuestTriggerData(QuestTrigger questTrigger)
    {
        string fullPath = eventTriggerDataSavePath + "/" + questTrigger.name + ".df";

        if (File.Exists(fullPath))
        {
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);

            QuestTriggerData eventTriggerData = bf.Deserialize(fileStream) as QuestTriggerData;

            fileStream.Close();

            return eventTriggerData;
        }
        else 
        {
            return new QuestTriggerData(questTrigger);
        }
    }


    public static void SaveMirrorData(Mirror mirror)
    {
        FileStream fileStream = new FileStream(mirrorDataSaveFile, FileMode.Create);

        MirrorData mirrorData = new MirrorData(mirror);

        bf.Serialize(fileStream, mirrorData);

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static MirrorData LoadMirrorData()
    {
        if (File.Exists(mirrorDataSaveFile))
        {
            FileStream fileStream = new FileStream(mirrorDataSaveFile, FileMode.Open);

            MirrorData mirrorData = bf.Deserialize(fileStream) as MirrorData;

            fileStream.Close();

            return mirrorData;
        }
        else
        {
            return null;
        }
    }


    public static void SaveFact(string factID)
    {
        List<string> factIDs = LoadFacts().ToList();

        if (factIDs.Contains(factID) == false)
            factIDs.Add(factID);

        FileStream fileStream = new FileStream(factDataSaveFile, FileMode.Create);

        bf.Serialize(fileStream, factIDs.ToArray());

        fileStream.Close();

        DataSavedEvent?.Invoke(null, EventArgs.Empty);
    }

    public static string[] LoadFacts()
    {
        if (File.Exists(factDataSaveFile))
        {
            FileStream fileStream = new FileStream(factDataSaveFile, FileMode.Open);

            string[] factsData = bf.Deserialize(fileStream) as string[];

            fileStream.Close();

            return factsData;
        }
        else
        {
            return new string[0];
        }
    }


    public static void DeleteAllSaveFiles()
    {
        var path = Application.persistentDataPath + "/saves";
        try
        {
            Directory.Delete(path, true);
            Debug.Log("All save files has been deleted");
        }
        catch (DirectoryNotFoundException)
        {
            Debug.Log($"Directory: '{path}' not Found");
        }
        catch (IOException)
        {
            Directory.Delete(path, true);
            Debug.Log("All save files has been deleted");
        }
        catch (UnauthorizedAccessException)
        {
            Directory.Delete(path, true);
            Debug.Log("All save files has been deleted");
        }
    }
}
