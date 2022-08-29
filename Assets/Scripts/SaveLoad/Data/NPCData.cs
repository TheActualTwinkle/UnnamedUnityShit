using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public readonly int relationshipWithPlayer;

    public NPCData(DialogueTrigger npc)
    {
        relationshipWithPlayer = npc.RelationshipWithPlayer;
    }
}
