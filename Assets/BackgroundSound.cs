using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundSound;
    void Awake()
    {
        // SoundFXManager.instance.PlaySound(backgroundSound, transform, 1f);
    }
}
