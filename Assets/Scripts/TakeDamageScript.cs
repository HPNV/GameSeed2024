using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TakeDamageScript : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float speed;
    [SerializeField] private float intensity;

    private PostProcessVolume _volume;
    private Vignette _vignette;

    private void Start()
    {
        _volume = GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings<Vignette>(out _vignette);

        if (!_vignette)
        {
            Debug.LogError("Error getting vignette");
        }
        else
        {
            _vignette.enabled.Override(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TakeDamageEffect());
        }
    }

    private IEnumerator TakeDamageEffect()
    {
        var temp = intensity;
        _vignette.enabled.Override(true);
        _vignette.intensity.Override(temp);

        yield return new WaitForSeconds(time);

        while (temp > 0)
        {
            temp -= speed;

            if (temp < 0) temp = 0;
            
            _vignette.intensity.Override(temp);

            yield return new WaitForSeconds(0.1f);
        }
        
        _vignette.enabled.Override(false);
        yield break;
    }

    public void StartEffect()
    {
        StartCoroutine(TakeDamageEffect());
    }
}
