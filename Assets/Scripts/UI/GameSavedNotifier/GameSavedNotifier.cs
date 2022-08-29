using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class GameSavedNotifier : MonoBehaviour
{
    [SerializeField] private float notifyInterval;
    private Animator animator;
    private float realtimeSinceNotify;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SaveLoadSystem.DataSavedEvent += OnGameSaved;
    }

    private void OnDisable()
    {
        SaveLoadSystem.DataSavedEvent -= OnGameSaved;
    }

    private void OnGameSaved(object s, EventArgs e)
    {
        if (Time.realtimeSinceStartup - realtimeSinceNotify >= notifyInterval)
        {
            realtimeSinceNotify = Time.realtimeSinceStartup;
            animator.SetTrigger("GameSaved");
        }
    }
}
