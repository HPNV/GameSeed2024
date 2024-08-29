using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public enum SpawnType
    {
        ExperienceOrb,
    }
    
    
    
    public class SpawnerManager
    {
        private static Dictionary<SpawnType, GameObject> _spawnDictionary = new()
        {
            { SpawnType.ExperienceOrb, Resources.Load<GameObject>("Prefabs/ExperienceOrb") }
        };
        
        public static GameObject Spawn(SpawnType spawnType, Vector3 position)
        {
            if (_spawnDictionary.TryGetValue(spawnType, out var prefab))
            {
                return Object.Instantiate(prefab, position, Quaternion.identity);
            }

            return null;
        }
    }
}