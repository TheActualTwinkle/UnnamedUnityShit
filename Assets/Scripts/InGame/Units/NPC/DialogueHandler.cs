using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Renderer), typeof(DialogueCloudImage))]
public class DialogueHandler : MonoBehaviour
{
    private static bool isReading;
    public static bool IsReading => isReading;

    private static DialogueTrigger currentSpeaker;
    public static DialogueTrigger CurrentSpeaker => currentSpeaker;

    private bool inDialogueRange;
    public bool InDialogueRange => inDialogueRange;

    private DialogueCloudImage dialogueCloudImage;
    private DialogueTrigger dialogueTrigger;
    private DialogueDisplayer dialogueDisplayer;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        dialogueCloudImage = GetComponent<DialogueCloudImage>();
        dialogueTrigger = GetComponentInParent<DialogueTrigger>();
        dialogueDisplayer = FindObjectOfType<DialogueDisplayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
        {
            inDialogueRange = true;
            dialogueCloudImage.MakeApparent();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
        {
            inDialogueRange = false;
            dialogueCloudImage.MakeTransparent(0.4f);
        }
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        DialogueDisplayer.DialogueDisplayedEvent += EndDialogue;

        currentSpeaker = dialogueTrigger;

        dialogueCloudImage.MakeTransparent(0.4f);
        isReading = true;

        dialogueDisplayer.DisplayDialogue(dialogueObject);

        animator.SetBool("isReading", isReading);
    }

    private void EndDialogue()
    {
        DialogueDisplayer.DialogueDisplayedEvent -= EndDialogue;

        currentSpeaker = null;

        dialogueTrigger.RemoveFirstAvailableDialogue();
        dialogueDisplayer.HideDialogue();

        isReading = false;
        animator.SetBool("isReading", isReading);
    }
}
