using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveFilesDeleter : MonoBehaviour
{
    [SerializeField] private bool deleteOnQuit;
    public bool DeleteOnQuit { get => deleteOnQuit; }

    private void OnApplicationQuit()
    {
        if (deleteOnQuit == true)
        {
            DeleteAllSaveFiles();
        }
    }

    public void DeleteAllSaveFiles()
    {
        SaveLoadSystem.DeleteAllSaveFiles();
    }
}
