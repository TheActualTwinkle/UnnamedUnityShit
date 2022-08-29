using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider2D))]
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableAsset playableAsset;
    [SerializeField] private string factCondition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player) == true)
        {
            if (string.IsNullOrEmpty(factCondition) == false && SaveLoadSystem.LoadFacts().Contains(factCondition) == false)
            {
                return;
            }
            CutscenePlayer.Instance.PlayCutscene(playableAsset);
        }
    }
}

