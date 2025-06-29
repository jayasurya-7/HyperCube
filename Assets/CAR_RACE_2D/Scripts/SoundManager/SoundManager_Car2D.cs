﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager_Car2D : MonoBehaviour {

    public static SoundManager_Car2D instance = null;

    public AudioSource efxSource;
    public AudioSource musicSource;

    public SpriteToggle_car2D[] Buttons;

    internal float currentState = 1.0f;
    internal bool mute = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //GameObject.DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        efxSource.volume = PlayerPrefs.GetFloat("SOUND_ON", 1f);
        musicSource.volume = PlayerPrefs.GetFloat("MUSIC_ON", 1f);

        mute = System.Convert.ToBoolean(PlayerPrefs.GetInt("MUTE_ALL", 0));
        SetSoundOnOff(mute);
    }

    public void PlaySound(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void ToggleSoundOnOff()
    {
        SetSoundOnOff(!mute);
    }

    public void SetSoundOnOff(bool off)
    {
        mute = off;
        PlayerPrefs.SetInt("MUTE_ALL", mute ? 1 : 0);

        efxSource.mute = off;
        musicSource.mute = off;

        foreach (SpriteToggle_car2D buttonToggle in Buttons)
            buttonToggle.SetToggleValue(System.Convert.ToInt32(!mute));
    }

	public bool GetSoundEfxMute()
	{
		return !efxSource.mute;

	}
}
