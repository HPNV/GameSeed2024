using System.Collections.Generic;
using Experience;
using UnityEngine;

namespace Manager
{
    public class ExperienceManager
    {
        private Queue<ExperienceOrbBehaviour> _experienceOrbPool;
        private GameObject _experienceOrbPrefab;
        
        public void Initialize()
        {
            _experienceOrbPool = new Queue<ExperienceOrbBehaviour>();
            
            _experienceOrbPrefab = Resources.Load<GameObject>("Prefabs/ExperienceOrb");
        }

        public void SpawnBatch(int spawnAmount, Vector2 position)
        {
            for (var i = 0; i < spawnAmount; i++)
            {
                Spawn(position);
            }
        }
        
        
        public void Spawn(Vector2 position)
        {
            if (_experienceOrbPool.Count == 0)
            {
                Object.Instantiate(_experienceOrbPrefab, position, Quaternion.identity);
                return;
            }
            
            var experienceOrb = _experienceOrbPool.Dequeue();
            
            experienceOrb.transform.position = position;
            experienceOrb.gameObject.SetActive(true);
            experienceOrb.Reset();
            
        }
        
        public void Despawn(ExperienceOrbBehaviour experienceOrb)
        {
            experienceOrb.gameObject.SetActive(false);
            _experienceOrbPool.Enqueue(experienceOrb);
        }
    }
}