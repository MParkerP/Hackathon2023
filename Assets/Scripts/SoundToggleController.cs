using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggleController : MonoBehaviour
{
    public GameObject effectsEnabledButton;
    public GameObject effectsMutedButton;

    public GameObject musicEnabledButton;
    public GameObject musicMutedButton;

    void Start()
    {
        if (AudioManager.IsMusicEnabled)
        {
            musicEnabledButton.SetActive(true);
            musicMutedButton.SetActive(false);
        }
        else
        {
            musicMutedButton.SetActive(true);
            musicEnabledButton.SetActive(false);
        }

        if (AudioManager.IsSoundEnabled)
        {
            effectsEnabledButton.SetActive(true);
            effectsMutedButton.SetActive(false);
        }
        else
        {
            effectsMutedButton.SetActive(true);
            effectsEnabledButton.SetActive(false);
        }


    }
}
