using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBacksound : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundSound;
    void Start()
    {
        SoundFXManager.instance.PlaySound(backgroundSound, transform, 0.5f);
    }
}
