using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    private AudioSource audioSource;
    
    [Range(0f, 120f)]
    [SerializeField] private float startDelay;  

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clip;
    }

    private void FixedUpdate()
    {
        if (audioSource.isPlaying == false)
            audioSource.PlayDelayed(startDelay);
    }
}