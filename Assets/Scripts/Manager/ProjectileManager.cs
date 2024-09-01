using System.Collections.Concurrent;
using System.Collections.Generic;
using Projectile;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class ProjectileManager
    {
        private readonly int _poolSize = 1000;
        private List<GameObject> _projectilePool;
        private GameObject _projectilePrefab;
        private ConcurrentDictionary<ProjectileType, ProjectileData> _projectileData;
        
        public void Initialize()
        {
            _projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
            _projectileData = new ConcurrentDictionary<ProjectileType, ProjectileData>();
            
            _projectileData.AddRange(new List<KeyValuePair<ProjectileType, ProjectileData>>()
            {
                new (ProjectileType.EnemyRanged, Resources.Load<ProjectileData>("Projectile/EnemyProjectile")),
            });
        }

        public void Spawn(ProjectileType type, Vector3 position, Vector2 direction, string targetTag)
        {
            var projectileObject = Object.Instantiate(_projectilePrefab, position, Quaternion.identity);
            var projectileBehaviour = projectileObject.GetComponent<ProjectileBehaviour>();
            projectileBehaviour.Direction = direction;
            projectileBehaviour.data = _projectileData[type];
            projectileBehaviour.TargetTag = targetTag;
            //
            // _projectilePool.Add(projectileObject);
            //
            // if (_projectilePool.Count > _poolSize)
            // {
            //     _projectilePool.RemoveAt(0);
            // }
        }
    }
}