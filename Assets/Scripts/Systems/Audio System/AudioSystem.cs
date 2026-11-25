using UnityEngine;
using System;

public enum SoundType
{
    SFX,
    UI,
    MUSIC,
}

[RequireComponent(typeof(AudioSource))]

public class AudioSystem : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _loopAudioSource;

    [SerializeField] private SoundList[] _soundList;
    public static AudioSystem Instance { get; private set; }
    private AudioSource _audioSource;


    public SoundList[] SoundList { get { return _soundList; } }

    public AudioSource AudioSource { get { return _audioSource; } } // check if the getter and setter works

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        // can't call DestroyOnLoad if in Editor Script...! 

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Instance.AudioSource.loop = false;
    }

    public void PlaySound(SoundType sound, int soundIndex, float volume = 1) 
    {
        //Remove static identifier

        //soundIndex plays the specific audio sound from the list?

        AudioClip[] clips = Instance.SoundList[(int)sound].Sounds;
        AudioClip clip = clips[soundIndex];
        Instance.AudioSource.PlayOneShot(clip, volume);
    }

    public void PlayLoopSound(SoundType sound, int soundIndex, float volume = 1)
    {
        if (_loopAudioSource != null)
        {
            _loopAudioSource.Stop();

            _loopAudioSource.loop = true;

            AudioClip[] clips = _soundList[(int)sound].Sounds;
            AudioClip clip = clips[soundIndex];

            _loopAudioSource.clip = clip;

            _loopAudioSource.Play();
        }
    }

    public void KillLoopSound(SoundType sound, int soundIndex, float volume = 1)
    {
        if (_loopAudioSource != null && _loopAudioSource == true)
        {
            _loopAudioSource.Stop();

            _loopAudioSource.loop = false;
        }
    }

    //*
    //Look into how to make a looping audio clip!
    //*
}

[Serializable]
public struct SoundList
{
    [SerializeField] public string Name;
    [SerializeField] private AudioClip[] _sounds;

    public AudioClip[] Sounds { get { return _sounds; } }
}
