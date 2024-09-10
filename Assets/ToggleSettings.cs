using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSettings : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private GameObject settings;
    
    public void toggleSettings()
    {
        SoundFXManager.instance.PlaySoundOnce(buttonClick, transform, 1f);
        // Toggle settings
        settings.SetActive(!gameObject.activeSelf);
    }
}
