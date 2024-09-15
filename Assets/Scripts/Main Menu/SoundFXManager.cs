using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private List<AudioSource> activeAudioSources = new List<AudioSource>();
    private List<AudioSource> gameAudioSources = new List<AudioSource>();
    private List<AudioSource> musicAudioSources = new List<AudioSource>();
    private List<AudioSource> audioAudioSources = new List<AudioSource>();

    private float masterVolume = 1.0f;
    
    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = value;
            UpdateAllVolumes(); // Update all volumes when master volume changes
        }
    }
    
    private float musicVolume = 1.0f;
    
    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;
            UpdateMusicVolumes(); // Update all volumes when music volume changes
        }
    }
    
    private float audioVolume = 1.0f;
    
    public float AudioVolume
    {
        get => audioVolume;
        set
        {
            audioVolume = value;
            UpdateAudioVolumes(); // Update all volumes when audio volume changes
        }
    }
    
    private float gameVolume = 1.0f;
    
    public float GameVolume
    {
        get => gameVolume;
        set
        {
            gameVolume = value;
            UpdateGameVolumes(); // Update all volumes when game volume changes
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void Initialize()
    {
        if (instance == null)
        {
            instance = new GameObject("SoundFXManager").AddComponent<SoundFXManager>();
            instance.soundFXObject = new GameObject("SoundFXObject").AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = masterVolume * musicVolume;
        audioSource.Play();
        audioSource.loop = true;
        musicAudioSources.Add(audioSource); // Add to active sources list
    }

    public void PlaySoundOnce(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = masterVolume * audioVolume;
        audioSource.Play();
        audioSource.loop = false;
        audioAudioSources.Add(audioSource); // Add to active sources list
    }

    public void PlaySound(string audioClip)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioClip);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = masterVolume * musicVolume;
        audioSource.Play();
        audioSource.loop = true;
        musicAudioSources.Add(audioSource); // Add to active sources list
    }
    
    public void PlayGameSound(string audioClip)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioClip);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = masterVolume * gameVolume;
        audioSource.Play();
        audioSource.loop = true;
        gameAudioSources.Add(audioSource); // Add to active sources list
    }

    public void PlayGameSoundOnce(string audioClip)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioClip);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = masterVolume * gameVolume;
        audioSource.Play();
        audioSource.loop = false;
        gameAudioSources.Add(audioSource); // Add to active sources list
        Destroy(audioSource.gameObject, clip.length);
    }

    public void PlayMusic(string audioClip)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioClip);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = masterVolume * musicVolume;
        audioSource.Play();
        audioSource.loop = true;
        musicAudioSources.Add(audioSource); // Add to active sources list
    }

    // This method will update the volume of all active audio sources
    private void UpdateAllVolumes()
    {

        if (gameAudioSources.Count > 0)
        {
            foreach (var audioSource in gameAudioSources)
            {
                if (audioSource != null)
                {
                    audioSource.volume = masterVolume * gameVolume;
                }
            }   
        }


        if (musicAudioSources.Count > 0)
        {
            foreach (var audioSource in musicAudioSources)
            {
                if (audioSource != null)
                {
                    audioSource.volume = masterVolume * musicVolume;
                }
            }
        }


        if (audioAudioSources.Count > 0)
        {
            foreach (var audioSource in audioAudioSources)
            {
                if (audioSource != null)
                {
                    audioSource.volume = masterVolume * audioVolume;
                }
            }
        }
    }
    
    private void UpdateMusicVolumes()
    {
        foreach (var audioSource in musicAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = masterVolume * musicVolume;
            }
        }
    }

    private void UpdateGameVolumes()
    {
        foreach (var audioSource in gameAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = masterVolume * gameVolume;
            }
        }
    }

    private void UpdateAudioVolumes()
    {
        foreach (var audioSource in audioAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = masterVolume * audioVolume;
            }
        }
    }
    
}
