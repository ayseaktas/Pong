using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManagement : MonoBehaviour
{

    AudioSource audioSource;
    
    public AudioClip bounceSoundEffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBounceSoundEffect(){
        audioSource.PlayOneShot(bounceSoundEffect);
    }

}
