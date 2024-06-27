using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[]
        musicSounds, sfxSounds;
    public AudioSource
        musicSource, sfxSource;

    private void Awake()
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
    private void Start()
    {
        PlayMusic("Theme");
    }
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
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
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;

    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.mute = false;
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.mute = false;
        sfxSource.volume = volume;
    }
}
