using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Renderer))]
public class UnitDialogueHandler : MonoBehaviour
{
    private static bool isReading;
    public static bool IsReading { get { return isReading; } }

    private static DialogueTrigger currentSpeaker;
    public static DialogueTrigger CurrentSpeaker { get => currentSpeaker; }

    private bool inDialogueRange;
    public bool InDialogueRange { get { return inDialogueRange; } }

    [SerializeField] private SpriteRenderer sprite;
    private DialogueTrigger dialogueTrigger;
    private DialogueDisplayer dialogueDisplayer;

    private Animator animator;

    private Color newCloudColor;

    [SerializeField] private float colorLerpTime;

    private void Start()
    {
        animator = GetComponent<Animator>();

        dialogueTrigger = GetComponentInParent<DialogueTrigger>();
        dialogueDisplayer = FindObjectOfType<DialogueDisplayer>();

        newCloudColor = sprite.color;
    }

    private void FixedUpdate()
    {
        LerpToNewColor(newCloudColor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
        {
            inDialogueRange = true;
            newCloudColor = new Color(1, 1, 1, 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
        {
            inDialogueRange = false;
            newCloudColor = new Color(1, 1, 1, 0.4f);
        }
    }

    public void StartDialogue(DialogueObject dialogueObject)
    {
        DialogueDisplayer.DialogueDisplayed += EndDialogue;

        currentSpeaker = dialogueTrigger;

        newCloudColor = new Color(1, 1, 1, 0.4f);
        isReading = true;

        dialogueDisplayer.DisplayDialogue(dialogueObject);

        animator.SetBool("isReading", isReading);
    }

    private void EndDialogue(object sender, EventArgs e)
    {
        DialogueDisplayer.DialogueDisplayed -= EndDialogue;

        currentSpeaker = null;

        dialogueTrigger.RemoveFirstAvailableDialogue();
        dialogueDisplayer.HideDialogue();

        isReading = false;
        animator.SetBool("isReading", isReading);
    }

    private void LerpToNewColor(Color newColor)
    {
        sprite.color = Color.Lerp(sprite.color, newColor, colorLerpTime * Time.fixedDeltaTime);
    }
}
