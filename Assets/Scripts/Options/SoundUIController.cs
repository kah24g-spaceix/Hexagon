using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIController : MonoBehaviour
{
    [SerializeField] Button _musicButton, _sfxButton;
    [SerializeField] Slider _musicSlider, _sfxSlider;

    private void Start()
    {
        _musicButton.onClick.AddListener(ToggleMusic);
        _sfxButton.onClick.AddListener(ToggleSFX);
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
