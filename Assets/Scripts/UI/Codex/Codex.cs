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
        {
            if (Input.GetKeyDown(Keybinds.KeyBinds[Actions.Codex]))
            {
                Open();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            Close();
        }
    }

    private void Open()
    {
        isOpen = true;

        animator.SetTrigger("StartCodex");
    }

    private void Close()
    {
        animator.SetTrigger("EndCodex");
    }

    // Button.
    private void BookmarkPage(GameObject page)
    {
        bookmarkPage = page;
    }

    // Button.
    private void UnbookmarkPage()
    {
        bookmarkPage = null;
    }

    // Animator.
    private void EnableInput()
    {
        isOpen = false;
    }
}
