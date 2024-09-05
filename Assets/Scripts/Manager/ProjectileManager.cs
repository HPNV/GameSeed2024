﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using Projectile;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class ProjectileManager
    {
        private Queue<Projectile.Projectile> _projectilePool;
        private GameObject _projectilePrefab;
        private Dictionary<ProjectileType, ProjectileData> _projectileData;
        
        public void Initialize()
        {
            _projectilePool = new Queue<Projectile.Projectile>();
            _projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
            _projectileData = new Dictionary<ProjectileType, ProjectileData>();
            
            _projectileData.AddRange(new List<KeyValuePair<ProjectileType, ProjectileData>>()
            {
                new (ProjectileType.EnemyRanged, Resources.Load<ProjectileData>("Projectile/EnemyProjectile")),
            });
        }

        public Projectile.Projectile Spawn(ProjectileType type, Vector3 position, Vector2 direction)
        {
            if (_projectilePool.Count == 0)
            {
                var projectileObject = Object.Instantiate(_projectilePrefab, position, Quaternion.identity);
                var projectileBehaviour = projectileObject.GetComponent<Projectile.Projectile>();
                projectileBehaviour.Direction = direction;
                projectileBehaviour.data = _projectileData[type];
                
                return projectileBehaviour;
            }
            
            var projectile = _projectilePool.Dequeue();
            
            projectile.transform.position = position;
            projectile.Direction = direction;
            projectile.data = _projectileData[type];
            projectile.gameObject.SetActive(true);

            return projectile;
        }
        
        public void Despawn(Projectile.Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
            _projectilePool.Enqueue(projectile);
        }
    }
}