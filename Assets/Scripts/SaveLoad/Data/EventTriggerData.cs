using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventTriggerData
{
    public readonly string eventID; 

    public EventTriggerData(EventTrigger eventTrigger)
    {
        eventID = eventTrigger?.WorldEvent?.EventID;
    }
}
