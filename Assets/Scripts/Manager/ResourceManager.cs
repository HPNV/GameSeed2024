using System.Collections.Concurrent;
using System.Collections.Generic;
using PickupableResource;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Manager
{
    public class ResourceManager
    {
        private GameObject _resourcePrefab;
        private ConcurrentDictionary<ResourceType, ResourceData> _resourceData;
            
        public void Initialize()
        {
            _resourcePrefab = Resources.Load<GameObject>("Prefabs/Resource");
            _resourceData = new ConcurrentDictionary<ResourceType, ResourceData>();
            
            _resourceData.AddRange(new List<KeyValuePair<ResourceType, ResourceData>>()
            {
                new (ResourceType.Water, Resources.Load<ResourceData>("PickupableResources/Water")),
                new (ResourceType.Sunlight, Resources.Load<ResourceData>("PickupableResources/Sunlight")),
                new (ResourceType.Mineral, Resources.Load<ResourceData>("PickupableResources/Mineral"))
            });
        }       
        
        public void Spawn(int spawnAmount, Vector3 position)
        {
            for (var i = 0; i < spawnAmount; i++)
            {
                foreach (var (_, resourceData) in _resourceData)
                {
                    if (resourceData.dropChance <= Random.Range(0, 100))
                    {
                        continue;
                    }
                    
                    var offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                    position += offset;
                    var resourceObject = Object.Instantiate(_resourcePrefab, position, Quaternion.identity);
                    var resourceBehaviour = resourceObject.GetComponent<ResourceBehaviour>();
                    
                    resourceBehaviour.resourceData = resourceData;
                }
            }
        }
    }
    
    public enum ResourceType
    {
        Water,
        Sunlight,
        Mineral
    }
}