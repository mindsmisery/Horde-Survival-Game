using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    [SerializeField] AudioClip _music;
    [SerializeField] AudioSource _sfxAudioSource;
    AudioMixerGroup _sfxGroup;
    AudioMixerGroup _musicGroup;

    const string MusicGroupName = "Music";
    const string SfxGroupName = "SFX";

    const string MusicVolume = "MusicVolume";
    const string SfxVolume = "SFXVolume";
    const string MasterVolume = "MasterVolume";

    void Init()
    {
        _musicGroup = _mixer.FindMatchingGroups(MusicGroupName)[0];
        _sfxGroup = _mixer.FindMatchingGroups(SfxGroupName)[0]; 
        PlayAudio(_music, SoundType.Music, 1.0f, true);
    }

    public void ChangeMasterVolume(float volume)
    {
        _mixer.SetFloat(MasterVolume, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Settings.MasterVolume", volume);
        PlayerPrefs.Save();
    }
    public void ChangeSFXVolume(float volume)
    {
        _mixer.SetFloat(SfxVolume, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Settings.SFXVolume", volume);
        PlayerPrefs.Save();
    }
    public void ChangeMusicVolume(float volume)
    {
        _mixer.SetFloat(MusicVolume, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Settings.MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        Init();
    }

    public enum SoundType
    {
        Music,
        SFX
    }

    public void PlayAudio(AudioClip audioClip, SoundType soundType, float volume, bool loop)
    {
        if (audioClip == null) return;

        if (soundType == SoundType.SFX && !loop)
        {
            _sfxAudioSource.outputAudioMixerGroup = _sfxGroup;
            _sfxAudioSource.PlayOneShot(audioClip, volume);
            return;
        }
        
        GameObject newAudioSource = new(audioClip.name + " Source");
        AudioSource audioSource = newAudioSource.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = loop;

        switch (soundType)
        {
            case SoundType.Music:
                audioSource.outputAudioMixerGroup = Instance._musicGroup;
                break;
            case SoundType.SFX:
                audioSource.outputAudioMixerGroup = Instance._sfxGroup;
                break;
            default:
                break;
        }

        audioSource.Play();
        if (!loop)
        {
            Destroy(audioSource.gameObject, audioClip.length);
        }
    }
}