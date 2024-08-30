using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ExperienceManager
    {
        private GameObject _experienceOrbPrefab;
        
        public void Initialize()
        {
            _experienceOrbPrefab = Resources.Load<GameObject>("Prefabs/ExperienceOrb");
        }
        
        public void Spawn(int spawnAmount, Vector3 position)
        {
            Debug.Log(_experienceOrbPrefab);
            for (var i = 0; i < spawnAmount; i++)
            {
                Object.Instantiate(_experienceOrbPrefab, position, Quaternion.identity);   
            }
        }
    }
}