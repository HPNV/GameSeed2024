using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Particles;
using Projectile;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class ParticleManager
    {
        private GameObject _projectilePrefab;
        private Dictionary<ParticleName, GameObject> _particleData;
        
        public void Initialize()
        {
            _particleData = new Dictionary<ParticleName, GameObject>
            {
                { ParticleName.SpeedUp, Resources.Load<GameObject>("Prefabs/Particles/SpeedUp") },
            };
        }
        public Particle Spawn(ParticleName type, Vector3 position, float duration)
        {
            var particleObject = Object.Instantiate(_particleData[type], position, Quaternion.identity);
            var particle = particleObject.GetComponent<Particle>();
            Debug.Log($"SPAWNED {particle}");
            particle.StartParticles(duration);
        

            return particle;
        }
    }

    public enum ParticleName
    {
        SpeedUp,
        Heal
    }
}