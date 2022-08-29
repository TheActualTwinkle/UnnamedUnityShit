

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private GameObject newGameConfirmMenu;

    private void Awake()
    {
        if (NewGameTuner.IsFirstExecute == true)
        {
            optionsMenu.Initialize();
        }
        else
        {
            var gameData = SaveLoadSystem.LoadGameData();
            Language.SetNewLanguage(gameData.savedLanguage);
            optionsMenu.Initialize();
        }
    }

    // Button.
    private void NewGame_Click()
    {
        if (NewGameTuner.IsNewGame == false)
        {
            newGameConfirmMenu.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            StartNewGame();
        }
    }

    // Button.
    private void LoadGame_Click()
    {
        if (NewGameTuner.IsNewGame == false)
        {
            var data = SaveLoadSystem.LoadPlayerData();
            SceneChanger.FadeToLevel(data.savedScene);
        }
        else
        {
            Debug.LogError("Save file not found");
        }
    }

    // Button.
    private void Options_Click()
    {
        optionsMenu.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Button.
    private void Quit_Click()
    {
        Debug.Log("Quit");

        SaveLoadSystem.SaveGameData();

        Application.Quit();
    }

    private void StartNewGame()
    {
        DeletePlayerSave();
        SceneChanger.FadeToLevel("StartScene");
    }

    private void DeletePlayerSave()
    {
        File.Delete(SaveLoadSystem.playerSaveFile);
    }
}
