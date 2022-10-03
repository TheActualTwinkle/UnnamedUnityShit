using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class InteractAccessor
{
    public static bool CanInteract { get => !IsSomethingBolockingInteraction(); }

    private static bool IsSomethingBolockingInteraction()
    {
        if (DialogueHandler.IsReading == true)
            return true;

        else if (Codex.IsOpen == true)
            return true;

        else if (Menus.IsGamePaused == true)
            return true;

        else if (SceneManager.GetActiveScene().name.Contains("Menu"))
            return true;
            
        else if (SceneChanger.IsLoading == true)
            return true;

        else if (PlayerController.Instance?.IsDashing == true)
            return true;

        else if (PlayerController.Instance == null)
            return true;

        else if (CutscenePlayer.Instance.IsPlaying == true)
            return true;

        else if (Map.IsOpen == true)
            return true;

        else
            return false;
    }
}
