using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        MusicVolume();
        SFXVolume();
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
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
}
