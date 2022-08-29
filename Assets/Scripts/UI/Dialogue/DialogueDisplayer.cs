using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Linq;

public class DialogueDisplayer : MonoBehaviour
{
    public static EventHandler DialogueDisplayed;

    private static bool isParagraphShowed;
    private static bool canSkipParagraph;

    [SerializeField] private Animator animator;

    [SerializeField] private float charDisplayRate;
    [SerializeField] private float delayBeforeStartPrintChars;

    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private readonly Dictionary<char, float> charDisplayTime = new Dictionary<char, float>()
    {
        { ',', 0.15f },
        { '-', 0.15f },
        { ':', 0.15f },
        { '"', 0.15f },

        { '.', 0.3f },
        { '?', 0.3f },
        { '!', 0.3f },
        { ';', 0.3f },
    };

    private Queue<string> paragraphs;
    private ChoiceButton[] choiceButtons;
    private DialogueObject currentDialogue;

    private IEnumerator _printChars;

    private string showingParagraph;

    private void Start()
    {
        choiceButtons = GetComponentsInChildren<ChoiceButton>(true);

        paragraphs = new Queue<string>();
    }

    private void Update()
    {
        if (UnitDialogueHandler.IsReading && Input.anyKeyDown && canSkipParagraph)
        {
            if (isParagraphShowed)
                ShowNextParagraph();
            else
                ShowFullParagraph();
        }
    }

    public void DisplayDialogue(DialogueObject dialogue)
    {
        currentDialogue = dialogue;

        animator.SetTrigger("DialogueStart");
        animator.SetBool("HasResponses", false);

        paragraphs.Clear();

        DialogueLanguageVariant languageVariant = dialogue.LanguageVariants.Where(x => x.language == Language.GetCurrentLanguage()).First();
        foreach (var p in languageVariant.Paragraphs)
        {
            paragraphs.Enqueue(p);
        }

        speakerNameText.text = dialogue.SpeakerName;
        dialogueText.text = null;

        ShowNextParagraph();
    }

    public void HideDialogue()
    {
        currentDialogue = null;

        animator.SetBool("HasResponses", false);
        animator.SetTrigger("DialogueOver");
        animator.ResetTrigger("DialogueStart");
    }

    private void ShowNextParagraph()
    {

        if (paragraphs.Count <= 0)
        {
            DialogueDisplayed?.Invoke(this, EventArgs.Empty);
            return;
        }

        dialogueText.text = null;

        showingParagraph = paragraphs.Dequeue();

        if (_printChars != null)
            StopCoroutine(_printChars);

        _printChars = PrintCharsInParagraph();
        StartCoroutine(_printChars);
    }

    private void ShowFullParagraph()
    {
        bool containseResponces = paragraphs.Count == 0 && currentDialogue.ResponseOptions.Count > 0;

        if (_printChars != null)
            StopCoroutine(_printChars);

        dialogueText.text = showingParagraph;

        if (containseResponces)
            ActivateChoiceButtons();
        else
            isParagraphShowed = true;
    }

    private IEnumerator PrintCharsInParagraph()
    {
        canSkipParagraph = false;

        bool containseResponces = paragraphs.Count == 0 && currentDialogue.ResponseOptions.Count > 0;
        animator.SetBool("HasResponses", false);

        yield return new WaitForSeconds(delayBeforeStartPrintChars);

        isParagraphShowed = false;

        for (int i = 0; i < showingParagraph.Length; i++)
        {
            dialogueText.text += showingParagraph[i];
            if (i >= 5 || showingParagraph.Length < 5)
                canSkipParagraph = true;

            yield return new WaitForSeconds(charDisplayTime.TryGetValue(showingParagraph[i], out float displayRate)? displayRate : charDisplayRate);
        }

        if (containseResponces)
            ActivateChoiceButtons();
        else
            isParagraphShowed = true;
    }

    private void ActivateChoiceButtons()
    {
        for (int i = 0; i < currentDialogue.ResponseOptions?.Count; i++)
        {
            ResponseOption response = currentDialogue.ResponseOptions[i];

            choiceButtons[i].Setup(response);
        }

        animator.SetBool("HasResponses", true);
    }

    // Animator.
    private void DisableAllChoiceButtons()
    {
        foreach (var button in choiceButtons)
        {
            button.Disable();
        }
    }
}
