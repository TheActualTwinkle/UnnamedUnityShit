using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(SaveFilesDeleter))]
public class DeleterInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveFilesDeleter deleter = (SaveFilesDeleter)target;
        if (GUILayout.Button("Delete All Save Files"))
        {
            deleter.DeleteAllSaveFiles();
        }
    }
}
#endif
