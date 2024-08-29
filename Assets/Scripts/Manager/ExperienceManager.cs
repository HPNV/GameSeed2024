using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ExperienceManager
    {
        private static GameObject _experienceOrbPrefab = Resources.Load<GameObject>("Prefabs/ExperienceOrb");
        
        public static void Spawn(int spawnAmount, Vector3 position)
        {
            for (var i = 0; i < spawnAmount; i++)
            {
                Object.Instantiate(_experienceOrbPrefab, position, Quaternion.identity);   
            }
        }
    }
}