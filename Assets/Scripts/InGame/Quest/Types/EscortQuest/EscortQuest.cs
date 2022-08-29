using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EscortQuest", menuName = "QuestObject/EscortQuest")]
public class EscortQuest : QuestObject
{
    [Header("EscortQuest-------------------------------------------------------------------------")]

    [SerializeField] private Unit whomToEscort;
    public Unit WhomToEscort { get => whomToEscort; }

    [SerializeField] private Vector3 whereToEscort;
    public Vector3 WhereToEscort { get => whereToEscort; }

    public string SceneToEscort { get => sceneToEscort; }
    [SerializeField] private string sceneToEscort;
            
    protected override void OnValidate()
    {
        base.OnValidate();

        whereToEscort.z = 0f;
    }

    public void OnEscorted(object s, EscortQuestInfo data)
    {
        if (data.sceneToEscort == sceneToEscort && data.whereToEscort == whereToEscort && data.whomToEscort.GetType() == whomToEscort.GetType())
        {
            QuestCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
