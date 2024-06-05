using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Theme");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            PlaySFX("MouseButtonDown");
        }
        if (Input.GetMouseButtonUp(0))
        {
            PlaySFX("MouseButtonUp");
        }
    }
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x=> x.name == name);

        if(sound == null) 
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
}
