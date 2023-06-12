using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonController : MonoBehaviour
{
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup effectsMixer;

    public void AMPlayButton()
    {
        AudioManager.instance.playButton();
    }

    public void AMPlayClick()
    {
        AudioManager.instance.playClick();
    }

    public void AMPlayAmbience()
    {
        AudioManager.instance.ambience.Play();
    }

    public void ToggleMusic()
    {
        AudioManager.IsMusicEnabled = !AudioManager.IsMusicEnabled;
        if (AudioManager.IsMusicEnabled)
        {
            musicMixer.audioMixer.SetFloat("MusicVolume", 0f);
        }
        else
        {
            musicMixer.audioMixer.SetFloat("MusicVolume", -80f);
        }
        
    }



    public void ToggleSoundEffects()
    {
        AudioManager.IsSoundEnabled = !AudioManager.IsSoundEnabled;
        if (AudioManager.IsSoundEnabled)
        {
            musicMixer.audioMixer.SetFloat("SoundEffectsVolume", 0f);
        }
        else
        {
            musicMixer.audioMixer.SetFloat("SoundEffectsVolume", -80f);
        }

    }






}
