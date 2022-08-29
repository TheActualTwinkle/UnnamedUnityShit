using System.IO;
using UnityEditor;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
    [ReadOnly]
    [SerializeField] private string buffName;
    public string BuffName { get => buffName; }

    [TextArea(2, 3)]
    [SerializeField] private string description;
    public string Description { get => description; }

    [SerializeField] private float duration;
    public float Duration { get => duration; protected set => duration = value; }

    private float currentDuration;
    public float CurrentDuration { get => currentDuration; protected set => currentDuration = value; }

    private void OnValidate()
    {
        NameBuffLikeFile();

        if (duration < 0)
            duration = 0;
    }

    public abstract void ActivateBuff(GameCharacter character, float currentDuration = 0f);

    public abstract bool TryDeactivateBuff(GameCharacter character);

    private void NameBuffLikeFile()
    {
#if UNITY_EDITOR

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        string extension = Path.GetExtension(assetPath);

        string fileName = assetPath.Substring(assetPath.LastIndexOf("Buffs/") + "Buffs/".Length);

        buffName = fileName;
        buffName = buffName.Substring(0, buffName.Length - extension.Length);
#endif
    }
}
