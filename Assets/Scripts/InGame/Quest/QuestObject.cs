using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class QuestObject : ScriptableObject
{
    [ReadOnly]
    [SerializeField] private string questID;
    public string QuestID { get => questID; }

    [ReadOnly]
    [SerializeField] private QuestType questType;
    public QuestType QuestType { get => questType; }

    [ReadOnly]
    [SerializeField] private bool isCompleted;

    [SerializeField] private List<QuestLanguageVariant> languageVariants;
    public List<QuestLanguageVariant> LanguageVariants { get => languageVariants; }

    public EventHandler QuestCompleted;

    private void Awake()
    {
        SetupLanguageVariants();
        SetQuestType();
    }

    protected virtual void OnValidate()
    {
        SetID();
        SetupLanguageVariants();
    }

    private void SetupLanguageVariants()
    {
        List<string> supportedLanguages = Enum.GetNames(typeof(Languages)).ToList();

        if (languageVariants == null)
        {
            languageVariants = new List<QuestLanguageVariant>(supportedLanguages.Count);
            for (int i = 0; i < supportedLanguages.Count; i++)
            {
                languageVariants.Add(new QuestLanguageVariant());
            }
        }

        if (LanguageVariants.Count < supportedLanguages.Count)
        {
            int missingVariantsCount = supportedLanguages.Count - LanguageVariants.Count;
            for (int i = 0; i < missingVariantsCount; i++)
            {
                languageVariants.Add(new QuestLanguageVariant());
            }
        }
        else if (LanguageVariants.Count > supportedLanguages.Count)
        {
            List<QuestLanguageVariant> presentedLanguages = LanguageVariants.ToList();
            List<QuestLanguageVariant> rightLanguages = presentedLanguages.Where(x => supportedLanguages.Contains(x.language.ToString())).DistinctBy(x => x.language).ToList();
            List<QuestLanguageVariant> removedVariants = presentedLanguages.Except(rightLanguages).ToList();

            languageVariants = languageVariants.Except(removedVariants).ToList();
        }

        for (int i = 0; i < supportedLanguages.Count; i++)
        {
            languageVariants[i].language = (Languages)(i + 1);
        }
    }

    private void SetQuestType()
    {
        string typeName = GetType().Name;
        int questIndex = typeName.IndexOf("Quest");

        string questTypeName = typeName.Substring(0, questIndex);

        questType = (QuestType)Enum.Parse(typeof(QuestType), questTypeName);
    }

    private void SetID()
    {
#if UNITY_EDITOR
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        string extension = Path.GetExtension(assetPath);

        string[] dialogueInfo = assetPath.Substring(assetPath.LastIndexOf("Quests/ScriptableObjects/") + "Quests/ScriptableObjects/".Length).Split('/');

        questID = null;
        for (int i = 0; i < dialogueInfo.Length; i++)
        {
            questID += dialogueInfo[i] + "/";
        }
        questID = questID.Substring(0, questID.Length - extension.Length - 1);
#endif
    }
}
    