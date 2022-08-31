using System;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using ExtensionMethods;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "DialogueObject", menuName = "DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [ReadOnly]
    [SerializeField] private string speakerName;
    public string SpeakerName { get => speakerName; }

    [ReadOnly]
    [SerializeField] private string dialogueID;
    public string DialogueID { get => dialogueID; }

    [Range(-100, 100)] [SerializeField] private int requiredRelationshipPoints;
    public int RequiredRelationshipPoints { get => requiredRelationshipPoints; }

    [SerializeField] private string requiredFact;
    public string RequiredFact { get => requiredFact; }

    [Header("-------------------------------------------------------------------------")]

    [SerializeField] private List<DialogueLanguageVariant> languageVariants;
    public List<DialogueLanguageVariant> LanguageVariants { get => languageVariants; }

    [Header("-------------------------------------------------------------------------")]

    [SerializeField] private List<ResponseOption> responseOptions;
    public List<ResponseOption> ResponseOptions { get => responseOptions; }

    private void OnEnable()
    {
        foreach (var dialogueVariant in languageVariants)
        {
            try
            {
                foreach (var paragraphs in dialogueVariant.Paragraphs)
                {
                    if (paragraphs.Length == 0)
                    {
                        Debug.LogWarning($"No text in '{DialogueID}' on '{SpeakerName}/{DialogueID}'. Language is {dialogueVariant.language} (id: {(int)dialogueVariant.language})");
                    }
                }
            }
            catch { }
        }
    }

    private void Awake()
    {
        SetupDialogueVariants();
    }

    private void OnValidate()
    {
        NameIDLikeFile();
        FillEmptyParagraphs();
        SetupDialogueVariants();
        SetupDialogueVariantsInResponse();

        if (responseOptions.Count > ChoiceButton.MAX_RESPONSE_OPTIONS)
            responseOptions.RemoveRange(ChoiceButton.MAX_RESPONSE_OPTIONS, responseOptions.Count - ChoiceButton.MAX_RESPONSE_OPTIONS);

        foreach (ResponseOption response in responseOptions)
        {
            response.SpeakerName = speakerName;
            ValidateDialogueForResponse(response);
        }
    }

    private void NameIDLikeFile()
    {
#if UNITY_EDITOR

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        string extension = Path.GetExtension(assetPath);

        string[] dialogueInfo = assetPath.Substring(assetPath.LastIndexOf("Dialogues/") + "Dialogues/".Length).Split('/');

        speakerName = Path.GetFileNameWithoutExtension(dialogueInfo[0]);
        dialogueID = null;
        for (int i = 1; i < dialogueInfo.Length; i++)
        {
            dialogueID +=  dialogueInfo[i] + "/";
        }
        dialogueID = dialogueID.Substring(0, dialogueID.Length - extension.Length - 1);

#endif
    }

    private void FillEmptyParagraphs()
    {
        foreach (var variant in languageVariants)
        {
            for (int i = 0; i < variant.Paragraphs.Count; i++)
            {
                if (string.IsNullOrEmpty(variant.Paragraphs[i]))
                {
                    variant.Paragraphs[i] = "DEFAULT TEXT \nLng: " + variant.language + ". Paragraph #" + i;
                }
            }
        }
    }

    private void SetupDialogueVariants()
    {
        List<string> supportedLanguages = Enum.GetNames(typeof(Languages)).ToList();

        if (languageVariants == null)
        {
            languageVariants = new List<DialogueLanguageVariant>(supportedLanguages.Count);
            for (int i = 0; i < supportedLanguages.Count; i++)
            {
                languageVariants.Add(new DialogueLanguageVariant());
            }
        }
        
        if (LanguageVariants.Count < supportedLanguages.Count)
        {
            int missingVariantsCount = supportedLanguages.Count - LanguageVariants.Count;
            for (int i = 0; i < missingVariantsCount; i++)
            {
                languageVariants.Add(new DialogueLanguageVariant());
            }
        }
        else if (LanguageVariants.Count > supportedLanguages.Count)
        {
            List<DialogueLanguageVariant> presentedLanguages = LanguageVariants.ToList();
            List<DialogueLanguageVariant> rightLanguages = presentedLanguages.Where(x => supportedLanguages.Contains(x.language.ToString())).DistinctBy(x => x.language).ToList();
            List<DialogueLanguageVariant> removedVariants = presentedLanguages.Except(rightLanguages).ToList();

            languageVariants = languageVariants.Except(removedVariants).ToList();
        }

        for (int i = 0; i < supportedLanguages.Count; i++)
        {
            languageVariants[i].language = (Languages)(i + 1);
        }
    }

    private void SetupDialogueVariantsInResponse()
    {
        List<string> supportedLanguages = Enum.GetNames(typeof(Languages)).ToList();

        foreach (var response in responseOptions)
        {
            if (response.LanguageVariants == null || response.LanguageVariants.Count == 0)
            {
                response.LanguageVariants = new List<ResponseLanguageVariant>(supportedLanguages.Count);
                for (int i = 0; i < supportedLanguages.Count; i++)
                {
                    response.LanguageVariants.Add(new ResponseLanguageVariant());
                }
            }

            if (response.LanguageVariants.Count < supportedLanguages.Count)
            {
                int missingVariantsCount = supportedLanguages.Count - response.LanguageVariants.Count;
                for (int i = 0; i < missingVariantsCount; i++)
                {
                    response.LanguageVariants.Add(new ResponseLanguageVariant());
                }
            }
            else if (response.LanguageVariants.Count > supportedLanguages.Count)
            {
                List<ResponseLanguageVariant> presentedLanguages = response.LanguageVariants.ToList();
                List<ResponseLanguageVariant> rightLanguages = presentedLanguages.Where(x => supportedLanguages.Contains(x.language.ToString())).DistinctBy(x => x.language).ToList();
                List<ResponseLanguageVariant> removedVariants = presentedLanguages.Except(rightLanguages).ToList();

                response.LanguageVariants = response.LanguageVariants.Except(removedVariants).ToList();
            }

            for (int i = 0; i < supportedLanguages.Count; i++)
            {
                response.LanguageVariants[i].language = (Languages)(i + 1);
            }
        }
    }

    private void ValidateDialogueForResponse(ResponseOption response)
    {
        if (response?.DialogueObject != null && response.DialogueObject.SpeakerName != SpeakerName)
        {
            Debug.LogError($"You trying to add wrong dialogue: '{response?.DialogueObject.DialogueID}' on {SpeakerName}/{DialogueID}. SpeakerNames are different");
            response.DialogueObject = null;
        }
    }
}
