using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutscenePlayer : MonoBehaviour
{
    public static CutscenePlayer Instance { get; private set; }
    public bool IsPlaying { get { return playableDirector.state == PlayState.Playing; } }

    [SerializeField] private PlayableDirector playableDirector;
         
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
    }

    public void PlayCutscene(PlayableAsset playableAsset)
    {
        if (playableDirector.state != PlayState.Playing)
        {
            playableDirector.Play(playableAsset);
        }
    }
}
