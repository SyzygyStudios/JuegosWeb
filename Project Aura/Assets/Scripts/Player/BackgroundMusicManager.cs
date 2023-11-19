using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip world1;
    [SerializeField] private AudioClip world2;
    [SerializeField] private AudioClip world3;
    [SerializeField] private AudioClip world4;
    [SerializeField] private AudioClip world5;
    [SerializeField] private AudioClip world6;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Mundo 1":
                PlayWorld1();
                break;
            case "Mundo 2":
                PlayWorld2();
                break;
            case "Mundo 3":
                PlayWorld3();
                break;
            case "Mundo 4":
                PlayWorld4();
                break;
            case "Mundo 5":
                PlayWorld5();
                break;
            case "Mundo 6":
                PlayWorld6();
                break;
            
        }
    }
    
    public void PlayWorld1()
    {
        audioSource.clip = world1;
        audioSource.Play();
    }
    public void PlayWorld2()
    {
        audioSource.clip = world2;
        audioSource.Play();
    }
    public void PlayWorld3()
    {
        audioSource.clip = world3;
        audioSource.Play();
    }
    public void PlayWorld4()
    {
        audioSource.clip = world4;
        audioSource.Play();
    }
    public void PlayWorld5()
    {
        audioSource.clip = world5;
        audioSource.Play();
    }
    public void PlayWorld6()
    {
        audioSource.clip = world6;
        audioSource.Play();
    }
    
}
