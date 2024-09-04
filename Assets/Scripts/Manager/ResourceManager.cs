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
        private Queue<ResourceBehaviour> _resourcePool;
        private GameObject _resourcePrefab;
        private Dictionary<ResourceType, ResourceData> _resourceData;
            
        public void Initialize()
        {
            _resourcePool = new Queue<ResourceBehaviour>();
            
            _resourcePrefab = Resources.Load<GameObject>("Prefabs/Resource");
            _resourceData = new Dictionary<ResourceType, ResourceData>();
            
            _resourceData.AddRange(new List<KeyValuePair<ResourceType, ResourceData>>()
            {
                new (ResourceType.Water, Resources.Load<ResourceData>("PickupableResources/Water")),
                new (ResourceType.Sunlight, Resources.Load<ResourceData>("PickupableResources/Sunlight")),
                new (ResourceType.Mineral, Resources.Load<ResourceData>("PickupableResources/Mineral"))
            });
        }       
        
        public void SpawnBatchWithChance(int spawnAmount, Vector2 position)
        {
            for (var i = 0; i < spawnAmount; i++)
            {
                SpawnWithChance(position);
            }
        }
        
        
        public void SpawnWithChance(Vector2 position)
        {
            foreach (var (_, resourceData) in _resourceData)
            {
                if (resourceData.dropChance <= Random.Range(0, 100))
                {
                    continue;
                }
                    
                if (_resourcePool.Count == 0)
                {
                    var resourceObject = Object.Instantiate(_resourcePrefab, position, Quaternion.identity);
                    var resourceBehaviour = resourceObject.GetComponent<ResourceBehaviour>();
                    
                    resourceBehaviour.resourceData = resourceData;
                    return;
                }
                
                var resource = _resourcePool.Dequeue();
                
                resource.transform.position = position;
                resource.gameObject.SetActive(true);
                resource.resourceData = resourceData;
                
            }
        }
        
        public void Despawn(ResourceBehaviour resource)
        {
            resource.gameObject.SetActive(false);
            _resourcePool.Enqueue(resource);
        }
        
    }
}