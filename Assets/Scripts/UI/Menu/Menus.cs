using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    private static bool isGamePaused;
    public static bool IsGamePaused { get => isGamePaused; }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private OptionsMenu optionsMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();

            else if (InteractAccessor.CanInteract)
                PauseGame();
        }
    }

    // Button.
    private void SaveAndQuit_Click()
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        SaveLoadSystem.SavePlayerData(PlayerController.Instance);

        SceneChanger.FadeToLevel("StartMenu");
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
        isGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        isGamePaused = false;
    }
}
