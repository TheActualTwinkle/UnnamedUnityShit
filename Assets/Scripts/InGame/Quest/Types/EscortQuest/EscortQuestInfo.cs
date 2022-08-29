using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EscortQuestInfo
{
    public readonly Unit whomToEscort;

    public readonly Vector3 whereToEscort;

    public readonly string sceneToEscort;

    public EscortQuestInfo(Unit whomToEscort, Vector3 whereToEscort, string sceneToEscort)
    {
        this.whomToEscort = whomToEscort;
        this.whereToEscort = whereToEscort;
        this.sceneToEscort = sceneToEscort;
    }
}
