using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectsAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip land;
    [SerializeField] private AudioClip pickingPower;
    [SerializeField] private AudioSource audioSourceMovement;
    [SerializeField] private AudioSource audioSourceEffects;
    [SerializeField] private bool _walking;

    void Start()
    {
        _walking = false;
    }

    public void PlayWalk()
    {
        if(!_walking)
        {
            audioSourceMovement.mute = true;
            _walking = true;
            audioSourceMovement.loop = true;
            audioSourceMovement.clip = walk;
            audioSourceMovement.Play();
            audioSourceMovement.pitch = 1 + Random.Range(-0.05f, 0.05f);
            audioSourceMovement.mute = false;
        }
    }
    
    public void PlayPickPower()
    {
        audioSourceEffects.mute = true;
        _walking = false;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = pickingPower;
        audioSourceEffects.Play();
        audioSourceMovement.pitch = 1 + Random.Range(-0.05f, 0.05f);
        audioSourceEffects.mute = false;
    }
    
    public void StopWalk()
    {
        _walking = false;
        if (audioSourceMovement.clip.name.Equals(walk.name))
        {
            audioSourceMovement.loop = false;
            audioSourceMovement.Stop();
        }
    }
    public void PlayJump()
    {
        audioSourceMovement.mute = true;
        _walking = false;
        audioSourceMovement.loop = false;
        audioSourceMovement.clip = jump;
        audioSourceMovement.Play();
        audioSourceMovement.pitch = 1 + Random.Range(-0.05f, 0.05f);
        audioSourceMovement.mute = false;
    }

    public void PlayLand()
    {
        audioSourceMovement.mute = true;
        _walking = false;
        audioSourceMovement.loop = false;
        audioSourceMovement.clip = land;
        audioSourceMovement.Play();
        audioSourceMovement.pitch = 1 + Random.Range(-0.05f, 0.05f);
        audioSourceMovement.mute = false;
    }
}
