using System;
using System.Collections;
using UnityEngine;

namespace Particles
{
    public class Particle : MonoBehaviour
    {
        public ParticleSystem ParticleSystem { get; set; }
        private Coroutine _despawnCoroutine;

        private void Start()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        public void StartParticles(float duration)
        {
            if (ParticleSystem == null)
                ParticleSystem = GetComponent<ParticleSystem>();
            ParticleSystem.Play();
            _despawnCoroutine = StartCoroutine(DespawnCoroutine(duration));
        }
        
        private IEnumerator DespawnCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            Despawn();
        }
        
        public void Despawn()
        {
            ParticleSystem.Stop();
            ParticleSystem.Clear();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            if (_despawnCoroutine != null)
                StopCoroutine(_despawnCoroutine);
        }
    }
}