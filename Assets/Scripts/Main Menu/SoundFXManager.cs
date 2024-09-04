using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private float masterVolume;
    private float musicVolume;
    private float audioVolume;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
    }

}
