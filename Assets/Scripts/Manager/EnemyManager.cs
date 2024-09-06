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
                {EnemyType.Explosive, Resources.Load<EnemyData>("Enemy/Explosive")}
            };
        }
        
        public GameObject Spawn(EnemyType type, Vector2 position)
        {
            if (_enemyPool.Count == 0)
            {
                var enemyObject = Object.Instantiate(_enemyPrefab, position, Quaternion.identity);
                var enemyBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
                enemyBehaviour.enemyData = _enemyData[type];
                
                return enemyObject;
            }
            
            var enemy = _enemyPool.Dequeue();
            
            enemy.transform.position = position;
            enemy.enemyData = _enemyData[type];
            enemy.gameObject.SetActive(true);
            enemy.Initialize();
            
            return enemy.gameObject;
        }
        
        public void Despawn(EnemyBehaviour enemy)
        {
            enemy.gameObject.SetActive(false);
            _enemyPool.Enqueue(enemy);
        }
    }
}