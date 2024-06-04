using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSound;

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x=> x.name == name);

        if(sound == null) 
        {
            Debug.Log("Sound Not Found");
        }
    }
}
