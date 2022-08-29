using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class NPCDialoguesData
{
    public readonly List<string> NPCDialogueIDs = new List<string>();

    public NPCDialoguesData(string[] dialogueIDs)
    {
        NPCDialogueIDs = dialogueIDs?.ToList();
    }
}
