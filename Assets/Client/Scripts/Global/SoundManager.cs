using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _music;
    
    private const string SOUND_KEY = "sound_key";
    
    public bool MenuSoundState { get; private set; }
    
    void Awake()
    {
        var soundState = PlayerPrefs.GetInt(SOUND_KEY, 1);
        MenuSoundState = soundState == 1; 
        
        if(!MenuSoundState) SwitchMusicState(MenuSoundState);
    }

    public void SwitchMusicState(bool active)
    {
        foreach (var music in _music)
        {
            music.mute = !active;
        } 
        
        int state = Convert.ToInt32(active);
        PlayerPrefs.SetInt(SOUND_KEY, state);
    }
}
