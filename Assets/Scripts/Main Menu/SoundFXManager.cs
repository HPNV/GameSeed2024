using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private float masterVolume = 1;
    private float musicVolume = 1;
    private float gameVolume = 1;
    private float audioVolume = 1;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void Initialize()
    {
        if(instance == null)
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
        // loop the sound
        audioSource.loop = true;    
    }
    
    public void PlaySoundOnce(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = masterVolume * audioVolume;
        audioSource.Play();
        // loop the sound
        audioSource.loop = false;    
        Destroy(audioSource.gameObject, audioClip.length);
    }

    public void PlayGameSound(AudioClip audioClip) {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = masterVolume * gameVolume;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void PlayGameSoundOnce(AudioClip audioClip) {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = masterVolume * gameVolume;
        audioSource.Play();
        audioSource.loop = false;
        Destroy(audioSource.gameObject, audioClip.length);
    }
 
}
