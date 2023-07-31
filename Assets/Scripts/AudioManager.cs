using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //audio manager singleton
    public static AudioManager instance { get; set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource clickSoundEffect;
    [SerializeField] private AudioSource buttonSoundEffect;
    [SerializeField] private AudioSource music;
    public AudioSource ambience;

    [SerializeField] private bool isSoundEnabled = true;
    [SerializeField] private bool isMusicEnabled = true;

    public bool IsSoundEnabled
    {
        get { return isSoundEnabled; }
        set { isSoundEnabled = value; }
    }

    public bool IsMusicEnabled
    {
        get { return isMusicEnabled; }
        set { isMusicEnabled = value; }
    }

    private void Start()
    {
    }

    public void playClick()
    {
        clickSoundEffect.Play();
    }

    public void playButton()
    {
        buttonSoundEffect.Play();
    }

}




