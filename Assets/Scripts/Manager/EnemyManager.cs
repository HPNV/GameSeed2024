using System.Collections.Concurrent;
using System.Collections.Generic;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class EnemyManager
    {
        private Queue<EnemyBehaviour> _enemyPool;
        private GameObject _enemyPrefab;
        private Dictionary<EnemyType, EnemyData> _enemyData;

        public void Initialize()
        {
            _enemyPool = new Queue<EnemyBehaviour>();
            _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
            
            _enemyData = new Dictionary<EnemyType, EnemyData>
            {
                {EnemyType.Melee, Resources.Load<EnemyData>("Enemy/Melee")},
                {EnemyType.Ranged, Resources.Load<EnemyData>("Enemy/Ranged")},
                {EnemyType.Explosive, Resources.Load<EnemyData>("Enemy/Explosive")},
                {EnemyType.MeleeFast, Resources.Load<EnemyData>("Enemy/MeleeFast")},
                {EnemyType.MeleeStrong, Resources.Load<EnemyData>("Enemy/MeleeHard")},
                {EnemyType.RangedTwo, Resources.Load<EnemyData>("Enemy/RangedTwo")},
                {EnemyType.RangedThree, Resources.Load<EnemyData>("Enemy/RangedThree")},
                {EnemyType.Large, Resources.Load<EnemyData>("Enemy/Large")},
            };
        }
        
        public GameObject Spawn(EnemyType type, Vector2 position, float multiplier)
        {
            if (_enemyPool.Count == 0)
            {
                var enemyObject = Object.Instantiate(_enemyPrefab, position, Quaternion.identity);
                var enemyBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
                enemyBehaviour.enemyData = _enemyData[type];
                enemyBehaviour.enemyData.health *= multiplier;
                enemyBehaviour.enemyData.attackPower *= multiplier;
                enemyBehaviour.enemyData.movementSpeed *= multiplier;
                return enemyObject;
            }
            
            var enemy = _enemyPool.Dequeue();
            
            enemy.transform.position = position;
            enemy.enemyData = _enemyData[type];
            enemy.enemyData.health *= multiplier;
            enemy.enemyData.attackPower *= multiplier;
            enemy.enemyData.movementSpeed *= multiplier;

            enemy.gameObject.SetActive(true);
            enemy.Initialize();
            
            return enemy.gameObject;
        }
        
        public void Despawn(EnemyBehaviour enemy)
        {
            enemy.gameObject.SetActive(false);
            enemy.enemyData = enemy.baseData;
            _enemyPool.Enqueue(enemy);
        }

        
    }
}