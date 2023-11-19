using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip land;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool _walking;

    void Start()
    {
        _walking = false;
    }

    public void PlayWalk()
    {
        if(!_walking)
        {
            audioSource.mute = true;
            _walking = true;
            audioSource.loop = true;
            audioSource.clip = walk;
            audioSource.Play();
            audioSource.mute = false;
        }
    }
    
    public void StopWalk()
    {
        _walking = false;
        if (audioSource.clip.name.Equals(walk.name))
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
    public void PlayJump()
    {
        audioSource.mute = true;
        _walking = false;
        audioSource.loop = false;
        audioSource.clip = jump;
        audioSource.Play();
        audioSource.mute = false;
    }

    public void PlayLand()
    {
        audioSource.mute = true;
        _walking = false;
        audioSource.loop = false;
        audioSource.clip = land;
        audioSource.Play();
        audioSource.mute = false;
    }
}
