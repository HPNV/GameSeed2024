using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        public List<ResourceBehaviour> ActiveResources { get; set; } = new();
        
        public double LuckModifier { get; set; } = 0;
        
        
        public void Initialize()
        {
            _resourcePool = new Queue<ResourceBehaviour>();
            
            _resourcePrefab = Resources.Load<GameObject>("Prefabs/Resource");
            _resourceData = new Dictionary<ResourceType, ResourceData>();
            
            _resourceData.AddRange(new List<KeyValuePair<ResourceType, ResourceData>>()
            {
                new (ResourceType.Water, Resources.Load<ResourceData>("PickupableResources/Water")),
                new (ResourceType.Sunlight, Resources.Load<ResourceData>("PickupableResources/Sunlight")),
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
                if (resourceData.dropChance <= Random.Range(0, 100) + LuckModifier)
                {
                    continue;
                }
                    
                if (_resourcePool.Count == 0)
                {
                    var resourceObject = Object.Instantiate(_resourcePrefab, position, Quaternion.identity);
                    var resourceBehaviour = resourceObject.GetComponent<ResourceBehaviour>();
                    
                    resourceBehaviour.resourceData = resourceData;
                    ActiveResources.Add(resourceBehaviour);
                    return;
                }
                
                var resource = _resourcePool.Dequeue();
                
                resource.transform.position = position;
                resource.gameObject.SetActive(true);
                resource.resourceData = resourceData;
                ActiveResources.Add(resource);
            }
        }


        public void Pickup(ResourceBehaviour resource)
        {
            if(resource.resourceData.resourceName == ResourceType.Water)
            {
                SingletonGame.Instance.homeBase.addWater(1);
            }
            else if(resource.resourceData.resourceName == ResourceType.Sunlight)
            {
                SingletonGame.Instance.homeBase.addSun(1);
            }
            
            Despawn(resource);
        }
        
        public void Despawn(ResourceBehaviour resource)
        {
            resource.gameObject.SetActive(false);
            ActiveResources.Remove(resource);
            _resourcePool.Enqueue(resource);
        }
        
    }
}