using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{

    void Start()
    {
        Slider[] sliders = gameObject.GetComponentsInChildren<Slider>();
        
        foreach (Slider slider in sliders)
        {
            if (slider.name == "Master Slider")
            {
                slider.value = SoundFXManager.instance.AudioVolume;
                slider.onValueChanged.AddListener(delegate { SoundFXManager.instance.MasterVolume = slider.value; });
            }
            else if (slider.name == "Music Slider")
            {
                slider.value = SoundFXManager.instance.MusicVolume;
                slider.onValueChanged.AddListener(delegate { SoundFXManager.instance.MusicVolume = slider.value; });
            }
            else if (slider.name == "Audio Slider")
            {
                slider.value = SoundFXManager.instance.AudioVolume;
                slider.onValueChanged.AddListener(delegate { SoundFXManager.instance.AudioVolume = slider.value; });
            }
            else if (slider.name == "Game Slider")
            {
                slider.value = SoundFXManager.instance.GameVolume;
                slider.onValueChanged.AddListener(delegate { SoundFXManager.instance.GameVolume = slider.value; });
            }
        }
        
    }
    
}
