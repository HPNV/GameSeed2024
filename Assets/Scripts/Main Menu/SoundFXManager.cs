using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private float masterVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float audioVolume = 1.0f;
    private float gameVolume = 0.6f;

    // List to keep track of active AudioSources
    private List<AudioSource> activeAudioSources = new List<AudioSource>();
    
    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = value;
            UpdateAllVolumes();
        }
    }
    
    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;
            UpdateAllVolumes();
        }
    }

    public float AudioVolume
    {
        get => audioVolume;
        set
        {
            audioVolume = value;
            UpdateAllVolumes();
        }
    }

    public float GameVolume
    {
        get => gameVolume;
        set
        {
            gameVolume = value;
            UpdateAllVolumes();
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
        if (instance is null)
        {
            instance = new GameObject("SoundFXManager").AddComponent<SoundFXManager>();
            instance.soundFXObject = new GameObject("SoundFXObject").AddComponent<AudioSource>();
        }
    }

    private void UpdateAllVolumes()
    {
        // Iterate through the list in reverse to safely remove destroyed audio sources
        for (int i = activeAudioSources.Count - 1; i >= 0; i--)
        {
            AudioSource audioSource = activeAudioSources[i];

            // Check if the AudioSource has been destroyed or is null
            if (audioSource == null)
            {
                activeAudioSources.RemoveAt(i);  // Remove it from the list
            }
            else
            {
                // Update the volume of the active AudioSource
                audioSource.volume = masterVolume * musicVolume; // Adjust this logic for each type of sound
            }
        }
    }

    public void PlaySound(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = masterVolume * musicVolume;
        audioSource.Play();
        audioSource.loop = true;

        // Add to the list of active AudioSources
        activeAudioSources.Add(audioSource);
    }
    
    public void PlaySoundOnce(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = masterVolume * audioVolume;
        audioSource.Play();
        audioSource.loop = false;

        // Add to the list of active AudioSources
        activeAudioSources.Add(audioSource);

        Destroy(audioSource.gameObject, audioClip.length); // Clean up after the sound finishes playing
    }

    public void PlayGameSound(string audioClip)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioClip);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = masterVolume * gameVolume;
        audioSource.Play();
        audioSource.loop = true;

        // Add to the list of active AudioSources
        activeAudioSources.Add(audioSource);
    }

    public void PlayGameSoundOnce(string audioClip)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioClip);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = masterVolume * gameVolume;
        audioSource.Play();
        audioSource.loop = false;

        // Add to the list of active AudioSources
        activeAudioSources.Add(audioSource);

        Destroy(audioSource.gameObject, clip.length); // Clean up after the sound finishes playing
    }

    public void PlayMusic(string audioClip)
    {
        AudioClip clip = Resources.Load<AudioClip>(audioClip);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = masterVolume * musicVolume;
        audioSource.Play();
        audioSource.loop = true;

        // Add to the list of active AudioSources
        activeAudioSources.Add(audioSource);
    }

    private void OnDestroy()
    {
        // Clean up AudioSources when the manager is destroyed
        foreach (AudioSource source in activeAudioSources)
        {
            if (source != null)
                Destroy(source.gameObject);
        }
    }
}
