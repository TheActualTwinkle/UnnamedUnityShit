using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class Codex : MonoBehaviour
{
    private static bool isOpen;
    public static bool IsOpen { get => isOpen; }

    [SerializeField] private GameObject bookmarkPageContainer;
    [ReadOnly]
    [SerializeField] private GameObject bookmarkPage;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (InteractAccessor.CanInteract)
            if (Input.GetKeyDown(Keybinds.KeyBinds[Actions.Codex]))
                OpenCodex();

        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
            CloseCodex();
    }

    public void OpenCodex()
    {
        isOpen = true;

        animator.ResetTrigger("EndCodex");
        animator.SetTrigger("StartCodex");
    }

    public void CloseCodex()
    {
        animator.ResetTrigger("StartCodex");
        animator.SetTrigger("EndCodex");
    }

    // Button.
    private void BookmarkPage(GameObject page)
    {
        bookmarkPage = page;
    }

    // Button.
    private void UnBookmarkPage()
    {
        bookmarkPage = null;
    }

    // Animator.
    private void EnableInput()
    {
        isOpen = false;
    }
}
