using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EventSystem))]
public class AutoDataSaver : MonoBehaviour
{
    // In seconds.
    public const float autoSaveTimeInterval = 6f * 60f;

    public static AutoDataSaver Instance { get; private set; }

    [SerializeField] private SaveFilesDeleter filesDeleter;

    private IEnumerator _autoSave;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _autoSave = AutoSaveAll();

        StartCoroutine(_autoSave);
    }

    private void OnApplicationQuit()
    {
        if (filesDeleter.DeleteOnQuit == true)
        {
            return;
        }

        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.RemoveAllBuffs();
            SaveLoadSystem.SavePlayerData(PlayerController.Instance);
        }
        SaveLoadSystem.SaveGameData();
    }

    private IEnumerator AutoSaveAll()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (PlayerController.Instance != null)
            {
                SaveLoadSystem.SavePlayerData(PlayerController.Instance);
            }
            SaveLoadSystem.SaveGameData();

            yield return new WaitForSeconds(autoSaveTimeInterval);
        }
    }
}
