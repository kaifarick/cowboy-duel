using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    
    
    private const string MUSIC_ENABLE_KEY = "mscEblK";
    private const string SOUND_ENABLE_KEY = "sudEblK";
    public bool MusicEnable { get; private set; }
    public bool SoundEnable { get; private set; }

    void Awake()
    {
        CheckAudioState();
    }

    private void CheckAudioState()
    {
        MusicEnable = PlayerPrefsExtensions.GetBool(MUSIC_ENABLE_KEY, true);
        SwitchMusicState(MusicEnable);
        
        SoundEnable = PlayerPrefsExtensions.GetBool(SOUND_ENABLE_KEY, true);
        SwitchSoundState(SoundEnable);
    }

    public void PlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    public void SwitchMusicState(bool active)
    {
        _musicSource.mute = !active;
        PlayerPrefsExtensions.SetBool(MUSIC_ENABLE_KEY, active);
    }
    
    public void SwitchSoundState(bool active)
    {
        _soundSource.mute = !active;
        PlayerPrefsExtensions.SetBool(SOUND_ENABLE_KEY, active);
    }
}
