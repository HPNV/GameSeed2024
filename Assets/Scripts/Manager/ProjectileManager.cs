﻿using System.Collections;
using System.Collections.Concurrent;
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
        private Dictionary<ProjectileName, ProjectileData> _projectileData;
        
        public void Initialize()
        {
            _projectilePool = new Queue<Projectile.Projectile>();
            _projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
            _projectileData = new Dictionary<ProjectileName, ProjectileData>
            {
                { ProjectileName.Enemy, Resources.Load<ProjectileData>("Projectile/EnemyProjectile") },
                { ProjectileName.Cactharn, Resources.Load<ProjectileData>("Projectile/CactharnProjectile") },
                { ProjectileName.Cobcorn, Resources.Load<ProjectileData>("Projectile/CobcornProjectile") },
                { ProjectileName.Weisshooter, Resources.Load<ProjectileData>("Projectile/WeisshooterProjectile") },
                { ProjectileName.Duricane, Resources.Load<ProjectileData>("Projectile/DuricaneProjectile") },
                { ProjectileName.Aloecure, Resources.Load<ProjectileData>("Projectile/AloecureProjectile") },
                { ProjectileName.ExplosiveMortar, Resources.Load<ProjectileData>("Projectile/ExplosiveMortar") },
                { ProjectileName.Fan, Resources.Load<ProjectileData>("Projectile/FanProjectile") }
            };
        }
        public Projectile.Projectile Spawn(ProjectileName type, Vector3 position, Vector2? direction = null, Vector2? target = null)
        {
            Projectile.Projectile projectile = GetOrCreateProjectile();
            projectile.transform.position = position;
            projectile.data = _projectileData[type];

            if (direction.HasValue)
            {
                projectile.Direction = direction.Value;
                projectile.Target = Vector2.zero;
            }
            else if (target.HasValue)
            {
                projectile.Target = target.Value;
                projectile.Direction = Vector2.zero;
            }
            else
            {
                projectile.Direction = Vector2.zero;
                projectile.Target = Vector2.zero;
            }

            projectile.gameObject.SetActive(true);
            projectile.Initialize();

            return projectile;
        }

        private Projectile.Projectile GetOrCreateProjectile()
        {
            if (_projectilePool.Count > 0)
            {
                return _projectilePool.Dequeue();
            }

            var projectileObject = Object.Instantiate(_projectilePrefab);
            return projectileObject.GetComponent<Projectile.Projectile>();
        }
        
        public void Despawn(Projectile.Projectile projectile)
        {
            projectile.StartCoroutine(TeleportAndDestroy(projectile));
        }
        
        private IEnumerator TeleportAndDestroy(Projectile.Projectile projectile)
        {
            if (!projectile.gameObject.activeSelf)
                yield break;
            
            projectile.transform.position = new Vector3(-1000, -1000, projectile.transform.position.z);
            yield return new WaitForSeconds(1);
            projectile.gameObject.SetActive(false);
            _projectilePool.Enqueue(projectile);
        }
    }

    public enum ProjectileName
    {
        Enemy,
        Cactharn,
        Cobcorn,
        Weisshooter,
        Duricane,
        Aloecure,
        ExplosiveMortar,
        Fan
    }
}