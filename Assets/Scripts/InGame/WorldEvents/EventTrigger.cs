using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EventTrigger : MonoBehaviour
{
    [SerializeField] private WorldEventObject worldEvent;
    public WorldEventObject WorldEvent { get => worldEvent; }

    private void Start()
    {
        if (NewGameTuner.IsNewGame == false)
            worldEvent = Resources.Load<WorldEventObject>("WorldEvents/" + SaveLoadSystem.LoadEventTriggerData(this).eventID);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
            worldEvent?.Execute(this, ref worldEvent);
    }
}
