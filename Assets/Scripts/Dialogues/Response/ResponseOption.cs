using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResponseOption
{
    [ReadOnly] private string speakerName;
    public string SpeakerName { get => speakerName; set => speakerName = value; }

    [SerializeField] private string requiredFactID;
    public string RequiredFactID { get => requiredFactID; }

    [SerializeField] private List<ResponseLanguageVariant> languageVariants;
    public List<ResponseLanguageVariant> LanguageVariants { get => languageVariants; set => languageVariants = value; }

    [SerializeField] private DialogueObject dialogueObject;
    public DialogueObject DialogueObject { get => dialogueObject; set => dialogueObject = value; }

    [SerializeField] private QuestObject questObject;
    public QuestObject QuestObject { get => questObject; }

    [SerializeField] private string addableFactID;
    public string AddableFactID { get => addableFactID; }

    [Range(-100, 100)]
    [SerializeField] private int relationshipsEffect;
    public int RelationshipsEffect { get => relationshipsEffect; }
}